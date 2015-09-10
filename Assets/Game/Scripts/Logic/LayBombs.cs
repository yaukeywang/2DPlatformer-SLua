/**
 * Lay bomb class.
 *
 * @filename  LayBombs.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-03
 */

using UnityEngine;
using System.Collections;
using SLua;

// Lay bombs class.
[CustomLuaClass]
public class LayBombs : MonoBehaviour
{
	// How many bombs the player has.
	public int m_bombCount = 0;

	// Sound for when the player lays a bomb.
	public AudioClip m_bombsAway = null;

	// Prefab of the bomb.
	public GameObject m_bomb = null;

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// The awake method.
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgLayBombs") || !m_bReady)
		{
			return;
		}
		
		m_cBehavior.Awake();
	}

	// Update is called once per frame
	void Update() 
	{
		if (m_bReady)
		{
			m_cBehavior.Update();
		}
	}

	// Get or set the bomb count of the player.
	public int BombCount
	{
		get
		{
			if (m_bReady)
			{
				// Lua gives double as default, need to improve in slua?
				double fBombCount = (double)m_cBehavior.GetData("m_nBombCount");
				return (int)fBombCount;
			}
			else
			{
				return 0;
			}
		}
		set
		{
			if (m_bReady)
			{
				m_cBehavior.SetData("m_nBombCount", value);
			}
		}
	}

	// Get or set the bomb is laid or not.
	public bool BombLaid
	{
		get
		{
			return m_bReady ? (bool)m_cBehavior.GetData("m_bBombLaid") : false;
		}
		set
		{
			if (m_bReady)
			{
				m_cBehavior.SetData("m_bBombLaid", value);
			}
		}
	}

	/**
     * Create a lua class instance for monobehavior instead of do a file.
     * 
     * @param string strFile - The lua class name.
     * @return bool - true if success, otherwise false.
     */
	private bool CreateClassInstance(string strClassName)
	{
		if (!m_cBehavior.CreateClassInstance(strClassName))
		{
			return false;
		}
		
		// Init variables.
		m_cBehavior.SetData("this", this);
		m_cBehavior.SetData("transform", transform);
		m_cBehavior.SetData("gameObject", gameObject);
		
		m_bReady = true;
		return true;
	}
}

/**
 * Rocket class.
 *
 * @filename  BombPickup.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-05
 */

using UnityEngine;
using System.Collections;

// The rocket class.
public class Rocket : MonoBehaviour
{
	// Prefab of explosion effect.
	public GameObject m_explosion = null;

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// The awake method.
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgRocket") || !m_bReady)
		{
			return;
		}
		
		m_cBehavior.Awake();
	}
	
	// Use this for initialization
	void Start()
	{
		if (m_bReady)
		{
			m_cBehavior.Start();
		}
	}

	// The trigger enter 2d event.
	void OnTriggerEnter2D(Collider2D cOther)
	{
		if (m_bReady)
		{
			m_cBehavior.OnTriggerEnter2D(cOther);
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
		m_cBehavior.SetData("m_cExplosion", m_explosion);
		
		m_bReady = true;
		return true;
	}
}

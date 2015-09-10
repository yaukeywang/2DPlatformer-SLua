/**
 * Destroyer class.
 *
 * @filename  Destroyer.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-05
 */

using UnityEngine;
using System.Collections;
using SLua;

// The destroyer class.
public class Destroyer : MonoBehaviour
{
	// Whether or not this gameobject should destroyed after a delay, on Awake.
	public bool m_destroyOnAwake = false;

	// The delay for destroying it on Awake.
	public float m_awakeDestroyDelay = 0.0f;

	// Find a child game object and delete it.
	public bool m_findChild = false;

	// Name the child object in Inspector.
	public string m_namedChild = string.Empty;

	// The destroy child game object function.
	private LuaFunction m_cDestroyChildObj = null;

	// The disable child game object function.
	private LuaFunction m_cDisableChildObj = null;

	// The destroy game object function.
	private LuaFunction m_cDestroyObj = null;
	
	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// The awake method.
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgDestroyer") || !m_bReady)
		{
			return;
		}
		
		m_cBehavior.Awake();
	}

	// Destroy this child gameobject, this can be called from an Animation Event.
	public void DestroyChildGameObject()
	{
		if (m_bReady)
		{
			m_cBehavior.CallMethod(ref m_cDestroyChildObj, "DestroyChildGameObject", m_cBehavior.GetChunk());
		}
	}

	// Destroy this child gameobject, this can be called from an Animation Event.
	public void DisableChildGameObject()
	{
		if (m_bReady)
		{
			m_cBehavior.CallMethod(ref m_cDisableChildObj, "DisableChildGameObject", m_cBehavior.GetChunk());
        }
	}

	// Destroy this gameobject, this can be called from an Animation Event.
	public void DestroyGameObject()
	{
		if (m_bReady)
		{
			m_cBehavior.CallMethod(ref m_cDestroyObj, "DestroyGameObject", m_cBehavior.GetChunk());
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

		m_cBehavior.SetData("m_bDestroyOnAwake", m_destroyOnAwake);
		m_cBehavior.SetData("m_fAwakeDestroyDelay", m_awakeDestroyDelay);
		m_cBehavior.SetData("m_bFindChild", m_findChild);
		m_cBehavior.SetData("m_strNamedChild", m_namedChild);
		
		m_bReady = true;
		return true;
	}
}

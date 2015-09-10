/**
 * Bomb class.
 *
 * @filename  Bomb.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-03
 */

using UnityEngine;
using System.Collections;
using SLua;

// The bomb class.
[CustomLuaClass]
public class Bomb : MonoBehaviour
{
	// Radius within which enemies are killed.
	public float m_bombRadius = 10.0f;

	// Force that enemies are thrown from the blast.
	public float m_bombForce = 100f;

	// Audioclip of explosion.
	public AudioClip m_boom = null;

	// Audioclip of fuse.
	public AudioClip m_fuse = null;

	// Fuse time.
	public float m_fuseTime = 1.5f;

	// Prefab of explosion effect.
	public GameObject m_explosion = null;

	// The "Explode" function.
	private SLua.LuaFunction m_cExplodeFunc = null;

	// The name of "Explode" function.
	private static readonly string m_strExplodeFunc = "Explode";

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();

	// The awake method.
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgBomb") || !m_bReady)
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

	/**
     * Call the lua function "Explode".
     * 
     * @param void.
     * @return void.
     */
	public void Explode()
	{
		if (m_bReady)
		{
			m_cBehavior.CallMethod(ref m_cExplodeFunc, m_strExplodeFunc, m_cBehavior.GetChunk());
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

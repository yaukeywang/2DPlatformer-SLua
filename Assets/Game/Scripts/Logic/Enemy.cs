/**
 * Enemy class.
 *
 * @filename  Enemy.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-03
 */

using UnityEngine;
using System.Collections;
using SLua;

// The enemy class.
[CustomLuaClass]
public class Enemy : MonoBehaviour
{
	// The speed the enemy moves at.
	public float m_moveSpeed = 2.0f;

	// How many times the enemy can be hit before it dies.
	public int m_HP = 2;

	// A sprite of the enemy when it's dead.
	public Sprite m_deadEnemy = null;

	// An optional sprite of the enemy when it's damaged.
	public Sprite m_damagedEnemy = null;

	// An array of audioclips that can play when the enemy dies.
	public AudioClip[] m_deathClips = null;

	// A prefab of 100 that appears when the enemy dies.
	public GameObject m_hundredPointsUI = null;

	// A value to give the minimum amount of Torque when dying.
	public float m_deathSpinMin = -100.0f;

	// A value to give the maximum amount of Torque when dying.
	public float m_deathSpinMax = 100f;

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// The awake method.
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgEnemy") || !m_bReady)
		{
			return;
		}
		
		m_cBehavior.Awake();
	}
	
	// Fixed Update is called by time.
	void FixedUpdate() 
	{
		if (m_bReady)
		{
			m_cBehavior.FixedUpdate();
		}
	}

	/**
     * Call the hurt function from lua.
     * 
     * @param void.
     * @return void.
     */
	public void Hurt()
	{
		m_HP--;
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

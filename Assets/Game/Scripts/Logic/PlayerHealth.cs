/**
 * Player health class.
 *
 * @filename  PlayerHealth.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-07
 */

using UnityEngine;
using System.Collections;
using SLua;

// The player health.
[CustomLuaClass]
public class PlayerHealth : MonoBehaviour
{
	// The player's health.
	public float m_health = 100.0f;

	// How frequently the player can be damaged.
	public float m_repeatDamagePeriod = 2.0f;

	// Array of clips to play when the player is damaged.
	public AudioClip[] m_ouchClips = null;

	// The force with which the player is pushed when hurt.
	public float m_hurtForce = 10.0f;

	// The amount of damage to take when enemies touch the player.
	public float m_damageAmount = 10.0f;

	// The update health bar function.
	private LuaFunction m_cUpdateHealthFunc = null;

	// The name of update health bar function.
	private static readonly string m_strUpdateHealthFunc = "UpdateHealthBar";

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// The awake method.
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgPlayerHealth") || !m_bReady)
		{
			return;
		}
		
		m_cBehavior.Awake();
	}

	// The collision enter 2d.
	void OnCollisionEnter2D(Collision2D cOther)
	{
		if (m_bReady)
		{
			m_cBehavior.OnCollisionEnter2D(cOther);
		}
	}

	/**
     * Update player health bar.
     * 
     * @param void.
     * @return void.
     */
	public void UpdateHealthBar()
	{
		if (m_bReady)
		{
			m_cBehavior.CallMethod(ref m_cUpdateHealthFunc, m_strUpdateHealthFunc, m_cBehavior.GetChunk());
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

/**
 * Player control class.
 *
 * @filename  PlayerControl.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-07
 */

using UnityEngine;
using System.Collections;
using SLua;

// The player control class.
[CustomLuaClass]
public class PlayerControl : MonoBehaviour
{
	// For determining which way the player is currently facing.
	[HideInInspector]
	public bool m_bFacingRight = true;

	// Condition for whether the player should jump.
	[HideInInspector]
	public bool m_bJump = false;
	
	// Amount of force added to move the player left and right.
	public float m_moveForce = 365.0f;

	// The fastest the player can travel in the x axis.
	public float m_maxSpeed = 5.0f;

	// Array of clips for when the player jumps.
	public AudioClip[] m_jumpClips;

	// Amount of force added when the player jumps.
	public float m_jumpForce = 1000.0f;

	// Array of clips for when the player taunts.
	public AudioClip[] m_taunts = null;

	// Chance of a taunt happening.
	public float m_tauntProbability = 50.0f;

	// Delay for when the taunt should happen.
	public float m_tauntDelay = 1.0f;

    // The taunt function.
	private LuaFunction m_cTauntFunc = null;

	// The name of taunt function.
	private static readonly string m_strTauntFunc = "Taunt";

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// The awake method.
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgPlayerControl") || !m_bReady)
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

	// Fixed Update is called by fixed time.
	void FixedUpdate() 
	{
		if (m_bReady)
		{
			m_cBehavior.FixedUpdate();
		}
	}

	// The destroy event.
	void OnDestroy()
	{
		if (m_bReady)
		{
			m_cBehavior.OnDestroy();
		}
	}

	/**
     * The taunt function.
     * 
     * @param void.
     * @return void.
     */
	public void Taunt()
	{
		if (m_bReady)
		{
			m_cBehavior.CallMethod(ref m_cTauntFunc, m_strTauntFunc, m_cBehavior.GetChunk());
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

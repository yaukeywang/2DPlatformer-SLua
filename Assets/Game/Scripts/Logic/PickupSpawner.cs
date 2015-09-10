/**
 * The pickup spawner class file.
 *
 * @filename  PickupSpawner.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-07
 */

using UnityEngine;
using System.Collections;
using SLua;

// The pickup spawner class.
[CustomLuaClass]
public class PickupSpawner : MonoBehaviour
{
	// Array of pickup prefabs with the bomb pickup first and health second.
	public GameObject[] m_pickups = null;

	// Delay on delivery.
	public float m_pickupDeliveryTime = 5.0f;

	// Smallest value of x in world coordinates the delivery can happen at.
	public float m_dropRangeLeft = 0.0f;

	// Largest value of x in world coordinates the delivery can happen at.
	public float m_dropRangeRight = 0.0f;

	// The health of the player, above which only bomb crates will be delivered.
	public float m_highHealthThreshold = 75.0f;

	// The health of the player, below which only health crates will be delivered.
	public float m_lowHealthThreshold = 25f;

	// The lua deliver pickup func.
	private LuaFunction m_cDeliverPickupFunc = null;

	// The name of deliver pickup func.
	private static readonly string m_strDeliverPickup = "DeliverPickup";

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// Use this for initialization
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgPickupSpawner") || !m_bReady)
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
     * Call the lua "DeliverPickup" function.
     * 
     * @param void.
     * @return void.
     */
	public void DeliverPickup()
	{
		if (m_bReady)
		{
			m_cBehavior.CallMethod(ref m_cDeliverPickupFunc, m_strDeliverPickup, m_cBehavior.GetChunk());
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

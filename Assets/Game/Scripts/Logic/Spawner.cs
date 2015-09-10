/**
 * The spawner class file.
 *
 * @filename  Spawner.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-01 
 */

using UnityEngine;
using System.Collections;

// The spawner class.
public class Spawner : MonoBehaviour
{
	// The amount of time between each spawn.
	public float m_spawnTime = 5.0f;

	// The amount of time before spawning starts.
	public float m_spawnDelay = 3.0f;

	// Array of enemy prefabs.
	public GameObject[] m_enemies = null;

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// Use this for initialization
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgSpawner") || !m_bReady)
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

	// The destroy event.
	void OnDestroy()
	{
		if (m_bReady)
		{
			m_cBehavior.OnDestroy();
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

		m_cBehavior.SetData("m_fSpawnTime", m_spawnTime);
		m_cBehavior.SetData("m_fSpawnDelay", m_spawnDelay);
		m_cBehavior.SetData("m_aEnemys", m_enemies);
		
		m_bReady = true;
		return true;
	}
}

/**
 * Back ground prop spawner class.
 *
 * @filename  BackgroundPropSpawner.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-05
 */

using UnityEngine;
using System.Collections;

// Back ground prop spawner class.
public class BackgroundPropSpawner : MonoBehaviour
{
	// The prop to be instantiated.
	public Rigidbody2D m_backgroundProp = null;

	// The x coordinate of position if it's instantiated on the left.
	public float m_leftSpawnPosX = 0.1f;

	// The x coordinate of position if it's instantiated on the right.
	public float m_rightSpawnPosX = 1.0f;

	// The lowest possible y coordinate of position.
	public float m_minSpawnPosY = 0.1f;

	// The highest possible y coordinate of position.
	public float m_maxSpawnPosY = 1.0f;

	// The shortest possible time between spawns.
	public float m_minTimeBetweenSpawns = 0.1f;

	// The longest possible time between spawns.
	public float m_maxTimeBetweenSpawns = 0.5f;

	// The lowest possible speed of the prop.
	public float m_minSpeed = 0.1f;

	// The highest possible speeed of the prop.
	public float m_maxSpeed = 0.1f;

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// The awake method.
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgBackgroundPropSpawner") || !m_bReady)
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

		m_cBehavior.SetData("m_cBackgroundProp", m_backgroundProp);
		m_cBehavior.SetData("m_fLeftSpawnPosX", m_leftSpawnPosX);
		m_cBehavior.SetData("m_fRightSpawnPosX", m_rightSpawnPosX);
		m_cBehavior.SetData("m_fMinSpawnPosY", m_minSpawnPosY);
		m_cBehavior.SetData("m_fMaxSpawnPosY", m_maxSpawnPosY);
		m_cBehavior.SetData("m_fMinTimeBetweenSpawns", m_minTimeBetweenSpawns);
		m_cBehavior.SetData("m_fMaxTimeBetweenSpawns", m_maxTimeBetweenSpawns);
		m_cBehavior.SetData("m_fMinSpeed", m_minSpeed);
		m_cBehavior.SetData("m_fMaxSpeed", m_maxSpeed);
		
		m_bReady = true;
		return true;
	}
}

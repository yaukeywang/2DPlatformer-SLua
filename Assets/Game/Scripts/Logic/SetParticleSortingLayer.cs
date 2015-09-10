/**
 * The set particle sorting layer class file.
 *
 * @filename  SetParticleSortingLayer.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-01 
 */

using UnityEngine;
using System.Collections;

public class SetParticleSortingLayer : MonoBehaviour
{
	// The name of the sorting layer the particles should be set to.
	public string m_sortingLayerName = string.Empty;

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// Use this for initialization
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgSetParticleSortingLayer") || !m_bReady)
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

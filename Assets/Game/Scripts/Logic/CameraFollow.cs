/**
 * The camera follow class file.
 *
 * @filename  CameraFollow.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-05
 */

using UnityEngine;
using System.Collections;
using SLua;

// The camera follow class.
[CustomLuaClass]
public class CameraFollow : MonoBehaviour
{
	// Distance in the x axis the player can move before the camera follows.
	public float m_xMargin = 1.0f;

	// Distance in the y axis the player can move before the camera follows.
	public float m_yMargin = 1.0f;

	// How smoothly the camera catches up with it's target movement in the x axis.
	public float m_xSmooth = 8.0f;

	// How smoothly the camera catches up with it's target movement in the y axis.
	public float m_ySmooth = 8.0f;

	// The maximum x and y coordinates the camera can have.
	public Vector2 m_maxXAndY;

	// The minimum x and y coordinates the camera can have.
	public Vector2 m_minXAndY;

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// Use this for initialization
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgCameraFollow") || !m_bReady)
		{
			return;
		}
		
		m_cBehavior.Awake();
	}

	// The fixupdate function.
	void FixedUpdate()
	{
		if (m_bReady)
		{
			m_cBehavior.FixedUpdate();
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

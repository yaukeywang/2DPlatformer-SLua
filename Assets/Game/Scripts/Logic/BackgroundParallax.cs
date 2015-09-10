/**
 * Background parallax class.
 *
 * @filename  BackgroundParallax.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-01
 */

using UnityEngine;
using System.Collections;

// The background parallax class.
public class BackgroundParallax : MonoBehaviour
{
	// Array of all the backgrounds to be parallaxed.
	public Transform[] m_backgrounds = null;

	// The proportion of the camera's movement to move the backgrounds by.
	public float m_parallaxScale = 1.0f;

	// How much less each successive layer should parallax.
	public float m_parallaxReductionFactor = 1.0f;

	// How smooth the parallax effect should be.
	public float m_smoothing = 0.1f;

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// Use this for initialization
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgBackgroundParallax") || !m_bReady)
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
	
	// Update is called once per frame
	void Update()
	{
		if (m_bReady)
		{
			m_cBehavior.Update();
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

		m_cBehavior.SetData("m_aBackgrounds", m_backgrounds);
		m_cBehavior.SetData("m_fParallaxScale", m_parallaxScale);
		m_cBehavior.SetData("m_fParallaxReductionFactor", m_parallaxReductionFactor);
		m_cBehavior.SetData("m_fSmoothing", m_smoothing);
		
		m_bReady = true;
		return true;
	}
}

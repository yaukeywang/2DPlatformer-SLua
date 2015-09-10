/**
 * Bomb pick up class.
 *
 * @filename  BombPickup.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-05
 */

using UnityEngine;
using System.Collections;

// The bomb pickup class.
public class BombPickup : MonoBehaviour
{
	// Sound for when the bomb crate is picked up.
	public AudioClip m_pickupClip = null;

	// Is ready or not.
	private bool m_bReady = false;
	
	// The lua behavior.
	private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
	// The awake method.
	void Awake()
	{
		// Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("LgBombPickup") || !m_bReady)
		{
            return;
        }
        
        m_cBehavior.Awake();
    }

	// The trigger enter 2d event.
	void OnTriggerEnter2D(Collider2D cOther)
	{
		if (m_bReady)
		{
			m_cBehavior.OnTriggerEnter2D(cOther);
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
		m_cBehavior.SetData("m_cPickupClip", m_pickupClip);
        
        m_bReady = true;
        return true;
    }
}

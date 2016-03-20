/**
 * Destroyer class.
 *
 * @filename  LgDestroyer.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-09-05
 */

using UnityEngine;
using System.Collections;

// The destroyer class.
public class LgDestroyer : MonoBehaviour
{
	// Whether or not this gameobject should destroyed after a delay, on Awake.
	public bool m_destroyOnAwake = false;

	// The delay for destroying it on Awake.
	public float m_awakeDestroyDelay = 0.0f;

	// Find a child game object and delete it.
	public bool m_findChild = false;

	// Name the child object in Inspector.
	public string m_namedChild = string.Empty;
	
	// The awake method.
	void Awake()
	{
        // If the gameobject should be destroyed on awake,
        if (m_destroyOnAwake)
        {
            if (m_findChild)
            {
                Destroy(transform.Find(m_namedChild).gameObject);
            }
            else
            {
                // ... destroy the gameobject after the delay.
                Destroy(gameObject, m_awakeDestroyDelay);
            }
        }
    }

	// Destroy this child gameobject, this can be called from an Animation Event.
	public void DestroyChildGameObject()
	{
        // Destroy this child gameobject, this can be called from an Animation Event.
        if (null!= transform.Find(m_namedChild).gameObject)
        {
            Destroy(transform.Find(m_namedChild).gameObject);
        }
    }

	// Destroy this child gameobject, this can be called from an Animation Event.
	public void DisableChildGameObject()
	{
        // Destroy this child gameobject, this can be called from an Animation Event.
        if (transform.Find(m_namedChild).gameObject.activeSelf)
        {
            transform.Find(m_namedChild).gameObject.SetActive(false);
        }
    }

	// Destroy this gameobject, this can be called from an Animation Event.
	public void DestroyGameObject()
	{
        // Destroy this gameobject, this can be called from an Animation Event.
        Destroy(gameObject);
    }
}

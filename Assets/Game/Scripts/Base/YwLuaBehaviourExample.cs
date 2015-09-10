/**
 * Lua behavior usage example.
 *
 * @filename  YwLuaBehaviourExample.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-08-23 
 */

using UnityEngine;
using SLua;
using System.Collections;

// The graphic unit animation agent.
[CustomLuaClass]
public class YwLuaBehaviourExample : MonoBehaviour 
{
	// The string.
	public string m_name = string.Empty;

    // Is ready or not.
    private bool m_bReady = false;

    // The lua behavior.
    private YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();
	
    // The awake method.
    void Awake()
    {
        // Directly creat a lua class instance to associate with this monobehavior.
		if (!CreateClassInstance("YwBehaviourExample") || !m_bReady)
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

	// The destroy event.
	void OnDestroy()
	{
		if (m_bReady)
		{
			m_cBehavior.OnDestroy();
		}
	}

    /**
     * Get the lua class instance (Actually a lua table).
     * 
     * @param void.
     * @return LuaTable - The class instance table..
     */
    public LuaTable GetInstance()
    {
        return m_cBehavior.GetChunk();
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
		m_cBehavior.SetData("m_strName", m_name);
        
        m_bReady = true;
        return true;
    }
}

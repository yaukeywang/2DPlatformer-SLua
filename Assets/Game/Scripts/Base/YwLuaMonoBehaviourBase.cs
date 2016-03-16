/**
 * The basic of lua mono behaviour class.
 *
 * @filename  YwLuaMonoBehaviourBase.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-11-28
 */

using UnityEngine;
using System.Collections;
using SLua;

// The basic of lua mono behaviour class.
// Do not use this class directly.
public class YwLuaMonoBehaviourBase : MonoBehaviour
{
    // Is ready or not for the lua script environment.
    protected bool m_bReady = false;

    // The lua behavior.
    protected YwLuaBehaviourBase m_cBehavior = new YwLuaBehaviourBase();

    /**
     * Get lua table, the associate lua table class can be accessed by other lua table class at lua side.
     * 
     * @param void.
     * @return LuaTable - The lua table class currently associated.
     */
    public LuaTable GetLuaTable()
    {
        if (!m_bReady)
        {
            return null;
        }

        return m_cBehavior.GetChunk();
    }

    /**
     * Create a lua class instance for monobehavior instead of do a file.
     * 
     * @param string strFile - The lua class name.
     * @return bool - true if success, otherwise false.
     */
    protected bool CreateClassInstance(string strClassName)
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

/**
 * The debug utility.
 *
 * @filename  YwDebug.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-08-23 
 */

using System;
using UnityEngine;
using System.Collections.Generic;
using SLua;

// The extend debug library.
[CustomLuaClass]
public class YwDebug
{
    /**
     * show waring log
     * 
     * @param object cMessage
     * @return void
     */
    public static void LogWarning(object cMessage)
    {
#if UNITY_EDITOR
        Debug.LogWarning(cMessage);
#else
        
#endif
    }
    
    /**
     * show error log
     * 
     * @param object cMessage
     * @return void
     */
    public static void LogError(object cMessage)
    {
#if UNITY_EDITOR
        Debug.LogError(cMessage);
#else
        
#endif
    }
    
    /**
     * show log
     * 
     * @param object cMessage
     * @return void
     */
    public static void Log(object cMessage)
    {
#if UNITY_EDITOR
        Debug.Log(cMessage);
#else
        
#endif
    }
    
    /**
     * show exception log
     * 
     * @param Exception cMessage
     * @return void
     */
    public static void LogException(Exception cMessage)
    {
#if UNITY_EDITOR
        Debug.LogException(cMessage);
#else
        
#endif
    }
}

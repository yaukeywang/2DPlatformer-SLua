/**
 * The lua mono data bridge class.
 *
 * @filename  YwLuaMonoDataBridge.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2016-03-17
 */

using UnityEngine;
using System.Collections;

// The lua mono data bridge class.
[RequireComponent(typeof(YwLuaMonoBehaviour))]
public class YwLuaMonoDataBridge : MonoBehaviour
{
    // The int data array.
    public int[] m_integers = null;

    // The float data array.
    public float[] m_floats = null;

    // The string data array.
    public string[] m_strings = null;
}

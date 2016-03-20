/**
 * The platform specifiy class.
 *
 * @filename  YwPlatform.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-12-29
 */

using UnityEngine;
using System.Collections;

// The current platform.
public static class YwPlatform
{
    // If currently build platform is windows.
    public static bool Windows
    {
#if UNITY_STANDALONE_WIN
        get { return true; }
#else
        get { return false; }
#endif
    }

    // If currently build platform is osx.
    public static bool OSX
    {
#if UNITY_STANDALONE_OSX
        get { return true; }
#else
        get { return false; }
#endif
    }

    // If currently build platform is iphone.
    public static bool iPhone
    {
#if UNITY_IPHONE
        get { return true; }
#else
        get { return false; }
#endif
    }

    // If currently build platform is android.
    public static bool Android
    {
#if UNITY_ANDROID
        get { return true; }
#else
        get { return false; }
#endif
    }
}

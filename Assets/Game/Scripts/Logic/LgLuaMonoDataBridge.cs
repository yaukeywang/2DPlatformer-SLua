/**
 * The lua mono data bridge class.
 *
 * @filename  LgLuaMonoDataBridge.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2016-03-17
 */

using UnityEngine;
using System.Collections;

// The lua mono data bridge class.
public class LgLuaMonoDataBridge : YwLuaMonoDataBridge
{
    // The sprite data array.
    public Sprite[] m_sprites = null;

    // The audio source data array.
    public AudioClip[] m_audioClips = null;
}

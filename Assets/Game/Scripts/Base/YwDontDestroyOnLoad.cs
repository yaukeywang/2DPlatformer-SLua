/**
 * The do not destroy on load script.
 *
 * @filename  YwDontDestroyOnLoad.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2016-01-24
 */

using UnityEngine;
using System.Collections;

// The do not destroy on load script.
public class YwDontDestroyOnLoad : MonoBehaviour
{
	// Use this for initialization
	void Awake()
    {
        DontDestroyOnLoad(gameObject);
	}
}

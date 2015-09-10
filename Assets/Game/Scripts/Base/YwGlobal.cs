/**
 * The YwGlobal class.
 *
 * @filename  YwGlobal.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-08-23
 */

using UnityEngine;
using System.Collections;

// The global object that not be destroyed.
public class YwGlobal : MonoBehaviour
{
    // The instance.
    private static YwGlobal m_cInstance = null;

    // Get the global instance.
	public static YwGlobal Instance
    {
        get
        {
            // Alloc one if have not.
            if (null == m_cInstance)
            {
                GameObject cObj = new GameObject("YwGlobal");
                m_cInstance = cObj.AddComponent<YwGlobal>();
            }

            return m_cInstance;
        }
    }

    // Awake to init.
    void Awake()
    {
        // Set property.
        DontDestroyOnLoad(gameObject);

        // Init manager.
        InitGlobalManager();
    }

    // Start to init.
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        YwLuaScriptMng.Instance.Update();
    }

    private void InitGlobalManager()
    {
        // The base support.
        YwLuaScriptMng.Instance.Initialize();
    }
}

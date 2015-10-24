/**
 * The lua script manager class.
 *
 * @filename  YwLuaScriptMng.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-05-06
 */

using UnityEngine;
using SLua;
using System.Collections;

// The lua script manager class.
public class YwLuaScriptMng
{
	// The lua service.
	private YwLuaSvr m_cLuaSvr = null;

    // The main update function.
    LuaFunction m_cUpdateFunc = null;

	// The global instance.
	private static YwLuaScriptMng m_cInstance = null;

	/**
     * Constructor.
     * 
     * @param void.
     * @return void.
     */
	private YwLuaScriptMng()
	{
		if (null != m_cInstance)
		{
			return;
		}

		m_cInstance = this;
	}

	/**
     * Destructor.
     * 
     * @param void.
     * @return void.
     */
	~YwLuaScriptMng()
	{
		m_cInstance = null;
	}

	/**
     * Get the global instance.
     * 
     * @param void.
     * @return YwLuaScriptMng - The instance.
     */
	public static YwLuaScriptMng Instance
	{
		get
		{
			if (null == m_cInstance)
			{
				new YwLuaScriptMng();
			}

			return m_cInstance;
		}
	}

	// Get the lua service.
	public YwLuaSvr LuaSvr
	{
		get
		{
			if (null == m_cLuaSvr)
			{
				m_cLuaSvr = new YwLuaSvr();
			}

			return m_cLuaSvr;
		}
	}

	public void Initialize()
	{
		YwLuaSvr cLuaSvr = LuaSvr;
		if (!cLuaSvr.Initialize(OnInitializeProgress, OnInitializedOk, false))
		{
			YwDebug.LogError("The lua environment can not be initialized!");
			return;
		}
	}

    /**
     * Update method.
     * 
     * @param void.
     * @return void.
     */
    public void Update()
    {
        // The main update logic entry.
        if (null == m_cUpdateFunc)
        {
            return;
        }

        // Try to call update.
        try
        {
            m_cUpdateFunc.call();
        }
        catch (System.Exception e)
        {
            YwDebug.LogError(YwLuaSvr.FormatException(e));
        }
    }

	/**
     * The initializing progress callback event.
     * 
     * @param int nProgress - The current initializing progress.
     * @return void.
     */
	private void OnInitializeProgress(int nProgress)
	{
		//YwDebug.Log(nProgress);
	}

	/**
     * The initializing complete callback event.
     * 
     * @param void.
     * @return void.
     */
	private void OnInitializedOk()
	{
		YwLuaSvr cLuaSvr = LuaSvr;
		if (!cLuaSvr.inited)
		{
			YwDebug.LogError("Create lua server failed!");
			return;
		}
		
		cLuaSvr.start("Main");
		
		// Get update function.
		m_cUpdateFunc = cLuaSvr.GetFunction("update");
		if (null == m_cUpdateFunc)
		{
			YwDebug.LogError("There is no update function in main file! Are you missing \'update()\'?");
			return;
		}
	}
}

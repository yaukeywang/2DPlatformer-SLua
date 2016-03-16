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
	/**
     * The binding progress delegate.
     * 
     * @param int nProgress - [0, 100], the current binding progress.
     * @return void.
     */
	public delegate void BindingProgress(int nProgress);
	
	// The lua service.
	private YwLuaSvr m_cLuaSvr = null;

	// The binding progress delegate variable.
	private BindingProgress m_cBindingProgress = null;

    // The main update function.
	private LuaFunction m_cUpdateFunc = null;

    // The main late update function.
	private LuaFunction m_cLateUpdateFunc = null;

    // The main fixed update function.
	private LuaFunction m_cFixedUpdateFunc = null;

    // The lite update function.
	private LuaFunction m_cLiteUpdateFunc = null;

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

    // Get if the lua script environment is inited ok.
    public bool Initialized
    {
        get
        {
            return LuaSvr.inited;
        }
    }

    /**
     * Initialize the lua script environment.
     * 
     * @param void.
     * @return void.
     */
    public void Initialize()
	{
		YwLuaSvr cLuaSvr = LuaSvr;
		if (!cLuaSvr.Initialize(OnInitializeProgress, OnInitializedOk, LuaSvrFlag.LSF_BASIC | LuaSvrFlag.LSF_EXTLIB))
		{
			YwDebug.LogError("The lua environment can not be initialized!");
			return;
		}
	}

	/**
     * Add an event for update binding progress.
     * 
     * @param BindingProgress cEvent - The binding progress delegate event.
     * @return void.
     */
	public void AddBindProgressEvent(BindingProgress cEvent)
	{
		m_cBindingProgress += cEvent;
	}

	/**
     * Remove an event for update binding progress.
     * 
     * @param BindingProgress cEvent - The binding progress delegate event.
     * @return void.
     */
	public void RemoveBindProgressEvent(BindingProgress cEvent)
	{
		m_cBindingProgress -= cEvent;
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
     * Late update method.
     * 
     * @param void.
     * @return void.
     */
    public void LateUpdate()
    {
        // The main late update logic entry.
        if (null == m_cLateUpdateFunc)
        {
            return;
        }

        // Try to call update.
        try
        {
            m_cLateUpdateFunc.call();
        }
        catch (System.Exception e)
        {
            YwDebug.LogError(YwLuaSvr.FormatException(e));
        }
    }

    /**
     * Fixed update method.
     * 
     * @param void.
     * @return void.
     */
    public void FixedUpdate()
    {
        // The main fixed update logic entry.
        if (null == m_cFixedUpdateFunc)
        {
            return;
        }

        // Try to call update.
        try
        {
            m_cFixedUpdateFunc.call();
        }
        catch (System.Exception e)
        {
            YwDebug.LogError(YwLuaSvr.FormatException(e));
        }
    }

    /**
     * Lite update method.
     * 
     * @param void.
     * @return void.
     */
    public void LiteUpdate()
    {
        // The main lite update logic entry.
        if (null == m_cLiteUpdateFunc)
        {
            return;
        }

        // Try to call update.
        try
        {
            m_cLiteUpdateFunc.call();
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
		// Debug.
		//YwDebug.Log(nProgress);

		// Notify all delegater.
		if (null != m_cBindingProgress)
		{
			m_cBindingProgress(nProgress);
		}
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
		m_cUpdateFunc = cLuaSvr.GetFunction("Update");
		if (null == m_cUpdateFunc)
		{
			YwDebug.LogError("There is no update function in main file! Are you missing \'Update()\'?");
			return;
		}

        // Get late update function.
        m_cLateUpdateFunc = cLuaSvr.GetFunction("LateUpdate");
        if (null == m_cLateUpdateFunc)
        {
            YwDebug.LogError("There is no late update function in main file! Are you missing \'LateUpdate()\'?");
            return;
        }

        // Get fixed update function.
        m_cFixedUpdateFunc = cLuaSvr.GetFunction("FixedUpdate");
        if (null == m_cFixedUpdateFunc)
        {
            YwDebug.LogError("There is no fixed update function in main file! Are you missing \'FixedUpdate()\'?");
            return;
        }

        // Get lite update function.
        m_cLiteUpdateFunc = cLuaSvr.GetFunction("LiteUpdate");
        if (null == m_cLiteUpdateFunc)
        {
            YwDebug.LogError("There is no lite update function in main file! Are you missing \'LiteUpdate()\'?");
            return;
        }
    }
}

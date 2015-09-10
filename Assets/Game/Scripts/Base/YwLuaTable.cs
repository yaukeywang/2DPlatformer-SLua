/**
 * The lua table operator base class.
 *
 * @filename  YwLuaTable.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-05-21
 */

using UnityEngine;
using SLua;
using LuaInterface;
using System.Collections;
using System;

// The lua table operator base class.
public class YwLuaTable
{
    // The destroy method name.
    private static readonly string ON_DESTROY = "OnDestroy";

    // The lua table of this behavior.
    private LuaTable m_cLuaTable = null;

    /**
     * Constructor.
     * 
     * @param void.
     * @return void.
     */
    public YwLuaTable(LuaTable cTable)
    {
        m_cLuaTable = cTable;
    }
    
    /**
     * Destructor.
     * 
     * @param void.
     * @return void.
     */
    ~YwLuaTable()
    {
        OnDestroy();
    }

    /**
     * On destroy method.
     * 
     * @param void.
     * @return void.
     */
    public void OnDestroy()
    {
        if (null == m_cLuaTable)
        {
            return;
        }
        
        // Call script first.
        LuaFunction cFunc = null;
        CallMethod(ref cFunc, ON_DESTROY, m_cLuaTable);
        
        // Dispose table.
        if (null != m_cLuaTable)
        {
            m_cLuaTable.Dispose();
            m_cLuaTable = null;
        }
    }

    /**
     * Get the lua code chunk (table).
     * 
     * @param void.
     * @return LuaTable - The chunk table.
     */
    public LuaTable GetChunk()
    {
        return m_cLuaTable;
    }

    /**
     * Set lua data to a lua table, used to communiate with other lua files.
     * 
     * @param string strName - The key name of the table.
     * @param object cValue - The value associated to the key.
     * @return void.
     */
    public void SetData(string strName, object cValue)
    {
        if ((null == m_cLuaTable) || string.IsNullOrEmpty(strName))
        {
			return;
        }

		m_cLuaTable[strName] = cValue;
    }

	/**
     * Set lua data to a lua table, used to communiate with other lua files.
     * This is used to set an array data to an sub-table.
     * 
     * @param string strName - The key name of the sub-table.
     * @param object cValue - The value associated to the key.
     * @return void.
     */
	public void SetData(string strName, object[] cArrayValue)
	{
		if (null == m_cLuaTable)
		{
			return;
		}

		if (string.IsNullOrEmpty(strName) || (null == cArrayValue) || (0 == cArrayValue.Length))
		{
			return;
		}

		LuaTable cSubTable = (LuaTable)m_cLuaTable[strName];
		if (null == cSubTable)
		{
			return;
		}

		int nLength = cArrayValue.Length;
		for (int i = 0; i < nLength; i++)
		{
			cSubTable[i + 1] = cArrayValue[i];
		}
	}

	/**
     * Set lua data to a lua table, used to communiate with other lua files.
     * 
     * @param int nIndex - The index of the table. (Start from 1.).
     * @param object cValue - The value associated to the key.
     * @return void.
     */
	public void SetData(int nIndex, object cValue)
	{
		if ((null == m_cLuaTable) || (nIndex < 1))
		{
			return;
		}
		
		m_cLuaTable[nIndex] = cValue;
	}

	/**
     * Set lua data to a lua table, used to communiate with other lua files.
     * This is used to set an array data to an sub-table.
     * 
     * @param int nIndex - The index of the sub-table. (Start from 1.)
     * @param object cValue - The value associated to the key.
     * @return void.
     */
	public void SetData(int nIndex, object[] cArrayValue)
	{
		if (null == m_cLuaTable)
		{
			return;
		}
		
		if ((nIndex < 1) || (null == cArrayValue) || (0 == cArrayValue.Length))
		{
			return;
		}
		
		LuaTable cSubTable = (LuaTable)m_cLuaTable[nIndex];
		if (null == cSubTable)
		{
			return;
		}
		
		int nLength = cArrayValue.Length;
		for (int i = 0; i < nLength; i++)
		{
			cSubTable[i + 1] = cArrayValue[i];
		}
	}

	/**
     * Get lua data from a lua table, used to communiate with other lua files.
     * 
     * @param string strName - The key name of the table.
     * @return object cValue - The value associated to the key.
     */
	public object GetData(string strName)
	{
		if ((null == m_cLuaTable) || string.IsNullOrEmpty(strName))
		{
			return null;
		}
		
		return m_cLuaTable[strName];
	}

	/**
     * Get lua data from a lua table, used to communiate with other lua files.
     * 
     * @param int nIndex - The index of the table.
     * @return object cValue - The value associated to the key.
     */
	public object GetData(int nIndex)
	{
		if ((null == m_cLuaTable) || (nIndex < 1))
		{
			return null;
		}
		
		return m_cLuaTable[nIndex];
	}

    /**
     * Call a lua method.
     * 
     * @param ref LuaFunction cResFunc - The result of the function, if the lua function calls ok, if it is not null, will call it instead of look up from table by strFunc.
     * @param string strFunc - The function name.
     * @return object - The number of result.
     */
    public object CallMethod(ref LuaFunction cResFunc, string strFunc)
    {
        // Check function first.
        if (null == cResFunc)
        {
            // Check params.
            if (string.IsNullOrEmpty(strFunc))
            {
                return null;
            }

            // Check table.
            if (null == m_cLuaTable)
            {
                return null;
            }

            // Check function.
            object cFuncObj = m_cLuaTable[strFunc];
            if ((null == cFuncObj) || !(cFuncObj is LuaFunction))
            {
                return null;
            }
            
            // Get function.
            cResFunc = (LuaFunction)cFuncObj;
            if (null == cResFunc)
            {
                return null;
            }
        }
        
        // Try to call this method.
        try
        {
            return cResFunc.call();
        }
        catch (Exception e)
        {
            YwDebug.LogError(YwLuaSvr.FormatException(e));
            cResFunc = null;
            return null;
        }
    }

    /**
     * Call a lua method.
     * 
     * @param ref LuaFunction cResFunc - The result of the function, if the lua function calls ok, if it is not null, will call it instead of look up from table by strFunc.
     * @param string strFunc - The function name.
     * @param object cParam - The param.
     * @return object - The returned value.
     */
    public object CallMethod(ref LuaFunction cResFunc, string strFunc, object cParam)
    {
        // Check function first.
        if (null == cResFunc)
        {
            // Check params.
            if (string.IsNullOrEmpty(strFunc))
            {
                return null;
            }
            
            // Check table.
            if (null == m_cLuaTable)
            {
                return null;
            }
            
            // Check function.
            object cFuncObj = m_cLuaTable[strFunc];
            if ((null == cFuncObj) || !(cFuncObj is LuaFunction))
            {
                return null;
            }
            
            // Get function.
            cResFunc = (LuaFunction)cFuncObj;
            if (null == cResFunc)
            {
                return null;
            }
        }
        
        // Try to call this method.
        try
        {
            return cResFunc.call(cParam);
        }
        catch (Exception e)
        {
            YwDebug.LogError(YwLuaSvr.FormatException(e));
            cResFunc = null;
            return null;
        }
    }

    /**
     * Call a lua method.
     * 
     * @param ref LuaFunction cResFunc - The result of the function, if the lua function calls ok, if it is not null, will call it instead of look up from table by strFunc.
     * @param string strFunc - The function name.
     * @param object cParam1 - The first param.
     * @param object cParam2 - The second param.
     * @return object - The number of result.
     */
    public object CallMethod(ref LuaFunction cResFunc, string strFunc, object cParam1, object cParam2)
    {
        // Check function first.
        if (null == cResFunc)
        {
            // Check params.
            if (string.IsNullOrEmpty(strFunc))
            {
                return null;
            }
            
            // Check table.
            if (null == m_cLuaTable)
            {
                return null;
            }
            
            // Check function.
            object cFuncObj = m_cLuaTable[strFunc];
            if ((null == cFuncObj) || !(cFuncObj is LuaFunction))
            {
                return null;
            }
            
            // Get function.
            cResFunc = (LuaFunction)cFuncObj;
            if (null == cResFunc)
            {
                return null;
            }
        }
        
        // Try to call this method.
        try
        {
            return cResFunc.call(cParam1, cParam2);
        }
        catch (Exception e)
        {
            YwDebug.LogError(YwLuaSvr.FormatException(e));
            cResFunc = null;
            return null;
        }
    }

    /**
     * Call a lua method.
     * 
     * @param ref LuaFunction cResFunc - The result of the function, if the lua function calls ok, if it is not null, will call it instead of look up from table by strFunc.
     * @param string strFunc - The function name.
     * @param object cParam1 - The first param.
     * @param object cParam2 - The second param.
     * @param object cParam3 - The third param.
     * @return object - The number of result.
     */
    public object CallMethod(ref LuaFunction cResFunc, string strFunc, object cParam1, object cParam2, object cParam3)
    {
        // Check function first.
        if (null == cResFunc)
        {
            // Check params.
            if (string.IsNullOrEmpty(strFunc))
            {
                return null;
            }
            
            // Check table.
            if (null == m_cLuaTable)
            {
                return null;
            }
            
            // Check function.
            object cFuncObj = m_cLuaTable[strFunc];
            if ((null == cFuncObj) || !(cFuncObj is LuaFunction))
            {
                return null;
            }
            
            // Get function.
            cResFunc = (LuaFunction)cFuncObj;
            if (null == cResFunc)
            {
                return null;
            }
        }
        
        // Try to call this method.
        try
        {
            return cResFunc.call(cParam1, cParam2, cParam3);
        }
        catch (Exception e)
        {
            YwDebug.LogError(YwLuaSvr.FormatException(e));
            cResFunc = null;
            return null;
        }
    }

    /**
     * Call a lua method.
     * 
     * @param ref LuaFunction cResFunc - The result of the function, if the lua function calls ok, if it is not null, will call it instead of look up from table by strFunc.
     * @param string strFunc - The function name.
     * @param params object[] aParams - The params.
     * @return object - The number of result.
     */
    public object CallMethod(ref LuaFunction cResFunc, string strFunc, params object[] aParams)
    {
        // Check function first.
        if (null == cResFunc)
        {
            // Check params.
            if (string.IsNullOrEmpty(strFunc))
            {
                return null;
            }
            
            // Check table.
            if (null == m_cLuaTable)
            {
                return null;
            }
            
            // Check function.
            object cFuncObj = m_cLuaTable[strFunc];
            if ((null == cFuncObj) || !(cFuncObj is LuaFunction))
            {
                return null;
            }
            
            // Get function.
            cResFunc = (LuaFunction)cFuncObj;
            if (null == cResFunc)
            {
                return null;
            }
        }
        
        // Try to call this method.
        try
        {
            if (null == aParams)
            {
                return cResFunc.call();
            }
            else
            {
                return cResFunc.call(aParams);
            }
        }
        catch (Exception e)
        {
            YwDebug.LogError(YwLuaSvr.FormatException(e));
            cResFunc = null;
            return null;
        }
    }
}

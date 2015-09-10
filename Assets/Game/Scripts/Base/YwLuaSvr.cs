/**
 * The lua service extend class.
 *
 * @filename  YwLuaSvr.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-08-23
 */

using UnityEngine;
using SLua;
using LuaInterface;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

// The lua service class, the global lua main entry.
public class YwLuaSvr : SLua.LuaSvr
{
	// The all include files.
	private static HashSet<string> m_aImportFiles = new HashSet<string>();

	/**
     * Constructor, and setup lua environment.
     * 
     * @param void.
     * @return void.
     */
	public YwLuaSvr() : base()
	{
		if (null == luaState)
		{
			Debug.LogError("Can not init lua state!");
			return;
		}

		// Set the new file loader.
		LuaState.loaderDelegate += LuaStreamFileLoader;

		// Overload the import function.
		LuaDLL.lua_pushcfunction(luaState.L, yw_lua_import);
		LuaDLL.lua_setglobal(luaState.L, "import");

        // Add package search path.
        SetupLuaPackagePath();
	}

	/**
     * Constructor with a lua file, use "main" as the entry, to setup lua environment.
     * 
     * @param void.
     * @return void.
     */
	public YwLuaSvr(string strMain) : base()
	{
		if (null == luaState)
		{
			Debug.LogError("Can not init lua state!");
			return;
		}

		// Set the new file loader.
		LuaState.loaderDelegate += LuaStreamFileLoader;

		// Overload the import function.
		LuaDLL.lua_pushcfunction(luaState.L, yw_lua_import);
		LuaDLL.lua_setglobal(luaState.L, "import");

        // Add package search path.
        SetupLuaPackagePath();

		// Finally start the lua script.
		start(strMain);
	}

	/**
     * Destructor.
     * 
     * @param void.
     * @return void.
     */
	~YwLuaSvr()
	{
		Dispose();
	}

	/**
     * Dispose the lua service.
     * 
     * @param void.
     * @return void.
     */
	public void Dispose()
	{

	}

	/**
     * Get the lua state handle.
     * 
     * @param void.
     * @return IntPtr - The handler.
     */
	public IntPtr Handle
	{
		get
		{
			return luaState.handle;
		}
	}

	/**
     * Get or set the lua value by the global table.
     * 
     * @param string strName - The lua key.
     * @return object - The value object.
     */
	public object this[string strName]
	{
		get
		{
			return luaState.getObject(strName);
		}
		set
		{
			luaState.setObject(strName, value);
		}
	}

	/**
     * Get the global function by the global table.
     * 
     * @param string strFuncName - The function name.
     * @return LuaFunction - The lua function object.
     */
	public LuaFunction GetFunction(string strFuncName)
	{
		return luaState.getFunction(strFuncName);
	}

    /**
     * Format a exception string.
     * 
     * @param System.Exception e - The exception object.
     * @return string - The result.
     */
    public static string FormatException(System.Exception e)
    {
        string strSource = string.IsNullOrEmpty(e.Source) ? "<no source>" : e.Source.Substring(0, e.Source.Length - 2);
        return string.Format("{0}\nLua (at {2})", e.Message, string.Empty, strSource);
    }

	/**
     * The new lua file loader.
     * 
     * @param string strFile - The file name.
     * @return byte[] - The loaded bytes.
     */
	private static byte[] LuaResourceFileLoader(string strFile)
	{
		if (string.IsNullOrEmpty(strFile))
		{
			return null;
		}

		strFile.Replace(".", "/");
		string strLuaPath = "Lua/" + strFile;
		TextAsset cLuaAsset = Resources.Load(strLuaPath, typeof(TextAsset)) as TextAsset;
		if (null == cLuaAsset)
		{
			return null;
		}

		return cLuaAsset.bytes;
	}

	/**
     * The new lua file loader.
     * 
     * @param string strFile - The file name.
     * @return byte[] - The loaded bytes.
     */
	private static byte[] LuaStreamFileLoader(string strFile)
	{
		if (string.IsNullOrEmpty(strFile))
		{
			return null;
		}
		
		strFile.Replace(".", "/");
		strFile += YwPathMng.FILE_AFFIX_LUA;

		string strLuaPath = YwPathMng.DATA_CATAGORY_LUA + Path.DirectorySeparatorChar + strFile;
		string strFullPath = YwPathMng.Instance.GetLoadUrl(strLuaPath);

		// Read from file.
		YwArchiveBinFile cArc = new YwArchiveBinFile();
		if (!cArc.Open(strFullPath, FileMode.Open, FileAccess.Read))
		{
			return null;
		}

		if (!cArc.IsValid())
		{
			return null;
		}

		int nContentLength = (int)cArc.GetStream().Length;
		byte[] aContents = new byte[nContentLength];
		cArc.ReadBuffer(ref aContents, nContentLength);
		cArc.Close();
		
		return aContents;
	}

    /**
     * Setup lua package search path.
     * 
     * @param void.
     * @return void.
     */
    private void SetupLuaPackagePath()
    {
        // Add package search path.
        // LPeg, sproto.
        string strSlash = Path.DirectorySeparatorChar.ToString();
		string strBaseLuaPath = YwPathMng.DATA_CATAGORY_LUA + strSlash;
		string strBasePkgPath = YwPathMng.Instance.GetLoadUrlForDir(strBaseLuaPath);
        string strPkgAffix = "?.lua";
        string strPkgPath = (string)luaState["package.path"];
        strPkgPath = strPkgPath + ";" + strBasePkgPath + "LPeg" + strSlash + strPkgAffix;
        strPkgPath = strPkgPath + ";" + strBasePkgPath + "sproto" + strSlash + strPkgAffix;
        luaState["package.path"] = strPkgPath;
    }

	/**
     * The new lua import package method.
     * 
     * @param IntPtr l - The lua state handler.
     * @return int - The state.
     */
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	protected internal static int yw_lua_import(IntPtr l)
	{
        if (IntPtr.Zero == l)
        {
            Debug.LogError("Invalid lua state pointer!");
            return 0;
        }

        LuaDLL.luaL_checktype(l, 1, LuaTypes.LUA_TSTRING);
        string str = LuaDLL.lua_tostring(l, 1);

        if (m_aImportFiles.Contains(str))
        {
            return 0;
        }

        m_aImportFiles.Add(str);
        return LuaState.import(l);
	}
}

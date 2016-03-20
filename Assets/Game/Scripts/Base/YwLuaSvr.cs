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
		// Do some initialization.
		// ...
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
     * Initialize lua environment.
     * 
     * @param Action<int> cProgress - The callback event for bind each C# classes, datatypes and functions. The parameter 'int' indicates the current progress.
     * @param Action cComplete - The callback event when all binding have beening completed.
     * @param LuaSvrFlag eFlag - The slua init flag, can be combined.
     * @return bool - true if lua can be initialized, otherwise false.
     */
    public bool Initialize(Action<int> cProgress, Action cComplete, LuaSvrFlag eFlag = LuaSvrFlag.LSF_BASIC)
	{
		if (null == cComplete)
		{
			Debug.LogError("You should give a lua bind complete delegate!");
			return false;
		}

		init(cProgress, () => {OnBindLuaComplete(); cComplete();}, eFlag);
		return true;
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
			return luaState[strName];
		}
		set
		{
			luaState[strName] = value;
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
     * The lua file loader resources folder.
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

        string strFinalFile = strFile.Replace('.', YwPathMng.PATH_SEPARATE_CHAR_SLASH);
        string strLuaPath =  YwPathMng.DATA_CATAGORY_LUA + YwPathMng.PATH_SEPARATE_CHAR_SLASH + strFinalFile;
		TextAsset cLuaAsset = Resources.Load(strLuaPath, typeof(TextAsset)) as TextAsset;
		if (null == cLuaAsset)
		{
			return null;
		}

		return cLuaAsset.bytes;
	}

	/**
     * The lua file loader from streaming asset folder.
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
		
        string strFinalFile = strFile.Replace('.', YwPathMng.PATH_SEPARATE_CHAR_SLASH);
        strFinalFile += YwPathMng.FILE_AFFIX_LUA;

        string strLuaPath = YwPathMng.DATA_CATAGORY_LUA + Path.DirectorySeparatorChar + strFinalFile;
		string strFullPath = YwPathMng.Instance.GetLoadUrl(strLuaPath);

        // Get bytes from file.
        byte[] aContents = null;
        if (strFullPath.StartsWith(YwPathMng.JAR_URL_PREFIX))
        {
            // Read from www. (Android platform.)
            WWW cDataPack = new WWW(strFullPath);
            while (!cDataPack.isDone)
            {
            }

            if (!string.IsNullOrEmpty(cDataPack.error))
            {
                Debug.LogError(cDataPack.error);
                return null;
            }

            int nDataLength = cDataPack.bytes.Length;
            if (0 == nDataLength)
            {
                Debug.LogError("No content in file: " + strFullPath);
            }

            aContents = new byte[nDataLength];
            Buffer.BlockCopy(cDataPack.bytes, 0, aContents, 0, nDataLength);

            cDataPack.Dispose();
            cDataPack = null;
        }
        else
        {
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
            aContents = new byte[nContentLength];
            cArc.ReadBuffer(ref aContents, nContentLength);
            cArc.Close();
        }

		return aContents;
	}

    /**
     * The lua file loader from persistent data folder.
     * 
     * @param string strFile - The file name.
     * @return byte[] - The loaded bytes.
     */
    private static byte[] LuaPersistentFileLoader(string strFile)
    {
        if (string.IsNullOrEmpty(strFile))
        {
            return null;
        }

        string strFinalFile = strFile.Replace('.', YwPathMng.PATH_SEPARATE_CHAR_SLASH);
        strFinalFile += YwPathMng.FILE_AFFIX_LUA;

        string strLuaPath = YwPathMng.DATA_CATAGORY_LUA + Path.DirectorySeparatorChar + strFinalFile;
        string strFullPathPersistent = YwPathMng.Instance.GetLoadUrlPersistentAssetPath(strLuaPath);
        string strFullPathStreaming = YwPathMng.Instance.GetLoadUrlStreamingAssetPath(strLuaPath);

        // Get bytes from file.
        byte[] aContents = null;
        if (!File.Exists(strFullPathPersistent))
        {
            // Load from streaming asset folder first.
            // Read from www. (Android platform has a prefix with jar:file://.)
            WWW cDataPack = new WWW(strFullPathStreaming);
            while (!cDataPack.isDone)
            {
            }

            if (!string.IsNullOrEmpty(cDataPack.error))
            {
                Debug.LogError(cDataPack.error);
                return null;
            }

            int nDataLength = cDataPack.bytes.Length;
            if (0 == nDataLength)
            {
                Debug.LogError("No content in file: " + strFullPathStreaming);
            }

            aContents = new byte[nDataLength];
            Buffer.BlockCopy(cDataPack.bytes, 0, aContents, 0, nDataLength);

            cDataPack.Dispose();
            cDataPack = null;

            // Sava data content to persistent data folder.
            YwPathMng.Instance.CreateDirIfNotExist(strFullPathPersistent, false);
            File.WriteAllBytes(strFullPathPersistent, aContents);
        }

        // Read file bytes from persistent path file.
        if (null == aContents)
        {
            aContents = File.ReadAllBytes(strFullPathPersistent);
        }

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
     * The callback event when bind all lua complete.
     * 
     * @param void.
     * @return void.
     */
	private void OnBindLuaComplete()
	{
		// Skip if already inited.
		if (!inited)
		{
			return;
		}

		// Check the lua state.
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
        return yw_lua_import_internal(l);
	}

    /**
     * The new lua import package method, same as LuaState.import.
     * 
     * @param IntPtr l - The lua state handler.
     * @return int - The state.
     */
    protected internal static int yw_lua_import_internal(IntPtr l)
    {
        try
        {
            LuaDLL.luaL_checktype(l, 1, LuaTypes.LUA_TSTRING);
            string str = LuaDLL.lua_tostring(l, 1);

            string[] ns = str.Split('.');

            LuaDLL.lua_pushglobaltable(l);

            for (int n = 0; n < ns.Length; n++)
            {
                LuaDLL.lua_getfield(l, -1, ns[n]);
                if (!LuaDLL.lua_istable(l, -1))
                {
                    return LuaObject.error(l, "expect {0} is type table", ns);
                }
                LuaDLL.lua_remove(l, -2);
            }

            LuaDLL.lua_pushnil(l);
            while (LuaDLL.lua_next(l, -2) != 0)
            {
                string key = LuaDLL.lua_tostring(l, -2);
                LuaDLL.lua_getglobal(l, key);
                if (!LuaDLL.lua_isnil(l, -1))
                {
                    LuaDLL.lua_pop(l, 1);
                    return LuaObject.error(l, "{0} had existed, import can't overload it.", key);
                }
                LuaDLL.lua_pop(l, 1);
                LuaDLL.lua_setglobal(l, key);
            }

            LuaDLL.lua_pop(l, 1);

            LuaObject.pushValue(l, true);
            return 1;
        }
        catch (Exception e)
        {
            return LuaObject.error(l, e);
        }
    }
}

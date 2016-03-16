/**
 * The path manager class.
 *
 * @filename  YwPathMng.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-08-30
 */

using UnityEngine;
using System.Collections;
using System.IO;

// The path manager class.
public class YwPathMng
{
	// The lua data folder name.
	public static readonly string DATA_CATAGORY_LUA = "Lua";

	// The lua file affix.
	public static readonly string FILE_AFFIX_LUA = ".lua";

	// The local file url prefix. (For assetbundle.)
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
	public static readonly string LOCAL_URL_PREFIX = "file:///";
#else
	public static readonly string LOCAL_URL_PREFIX = "file://";
#endif

    // The android jar file prefix.
    public static readonly string JAR_URL_PREFIX = "jar:file://";

    // The slash of path separate char.
    public static readonly char PATH_SEPARATE_CHAR_SLASH = '/';

    // The backlash kind of path separate char.
    public static readonly char PATH_SEPARATE_CHAR_BACKLASH = '\\';

	// The asset path in persistent asset path.
	private string m_strPersistAssetPath = string.Empty;
	
	// The asset path in streaming asset path.
	private string m_strStreamAssetPath = string.Empty;
	
	// The asset path in caching path.
	private string m_strCachingAssetPath = string.Empty;

	// The global instance.
	private static YwPathMng m_cInstance = null;

	/**
     * Constructor.
     * 
     * @param void.
     * @return void.
     */
	private YwPathMng()
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
	~YwPathMng()
	{
		m_cInstance = null;
	}

	/**
     * Get the global instance.
     * 
     * @param void.
     * @return YwPathMng - The instance.
     */
	public static YwPathMng Instance
	{
		get
		{
			if (null == m_cInstance)
			{
				new YwPathMng();
			}
			
			return m_cInstance;
		}
	}

	// Get persistent assets path.
	public string PersistentAssetsPath
	{
		get
		{
			if (string.IsNullOrEmpty(m_strPersistAssetPath))
			{
				m_strPersistAssetPath = Application.persistentDataPath + Path.DirectorySeparatorChar;
			}
			
			return m_strPersistAssetPath;
		}
	}

	// Get streaming assets path.
	public string StreamingAssetsPath
	{
		get
		{
			if (string.IsNullOrEmpty(m_strStreamAssetPath))
			{
				m_strStreamAssetPath = Application.streamingAssetsPath + Path.DirectorySeparatorChar;
			}
			
			return m_strStreamAssetPath;
		}
	}

	// Get caching assets path.
	public string CachingAssetsPath
	{
		get
		{
			if (string.IsNullOrEmpty(m_strCachingAssetPath))
			{
				m_strCachingAssetPath = Application.temporaryCachePath + Path.DirectorySeparatorChar;
			}
			
			return m_strCachingAssetPath;
		}
	}

	/**
     * Get the final load url.
     * 
     * @param string strPathName - The path name of the file with dir except the base url.
     * @return string - The final full url load string.
     */
	public string GetLoadUrl(string strPathName)
	{
		string strFilePath = PersistentAssetsPath + strPathName;
		if (File.Exists(strFilePath))
		{
			return strFilePath;
		}
		else
		{
			strFilePath = StreamingAssetsPath + strPathName;
			return strFilePath;
		}
	}

    /**
     * Get the final load url at persistent asset path.
     * 
     * @param string strPathName - The path name of the file with dir except the base url.
     * @return string - The final full url load string.
     */
    public string GetLoadUrlPersistentAssetPath(string strPathName)
    {
        string strFilePath = PersistentAssetsPath + strPathName;
        return strFilePath;
    }

    /**
     * Get the final load url at streaming asset path.
     * 
     * @param string strPathName - The path name of the file with dir except the base url.
     * @return string - The final full url load string.
     */
    public string GetLoadUrlStreamingAssetPath(string strPathName)
    {
        string strFilePath = StreamingAssetsPath + strPathName;
        return strFilePath;
    }
	
	/**
     * Get the final load url for directory.
     * 
     * @param string strPathName - The path dir name of the file with dir except the base url.
     * @return string - The final full url load string for the path dir.
     */
	public string GetLoadUrlForDir(string strPathName)
	{
		string strFilePath = PersistentAssetsPath + strPathName;
		if (Directory.Exists(strFilePath))
		{
			return strFilePath;
		}
		else
		{
			strFilePath = StreamingAssetsPath + strPathName;
			return strFilePath;
		}
	}

    /**
     * Get the final load url for directory at persistent asset path.
     * 
     * @param string strPathName - The path dir name of the file with dir except the base url.
     * @return string - The final full url load string for the path dir.
     */
    public string GetLoadUrlForDirPersistentAssetPath(string strPathName)
    {
        string strFilePath = PersistentAssetsPath + strPathName;
        return strFilePath;
    }

    /**
     * Get the final load url for directory at streaming asset path.
     * 
     * @param string strPathName - The path dir name of the file with dir except the base url.
     * @return string - The final full url load string for the path dir.
     */
    public string GetLoadUrlForDirStreamingAssetPath(string strPathName)
    {
        string strFilePath = StreamingAssetsPath + strPathName;
        return strFilePath;
    }

    /**
     * Copy file from a source path to a destination path, create directory if not exist, override the file if already exist.
     * 
     * @param string strSrcFilePath - The source path.
     * @param string strDstFilePath - The destination path.
     * @return bool - true if success, otherwise false.
     */
    public bool CopyFile(string strSrcFilePath, string strDstFilePath)
    {
        if (!File.Exists(strSrcFilePath))
        {
            return false;
        }

        if (!CreateDirIfNotExist(strDstFilePath, false))
        {
            return false;
        }

        File.Copy(strSrcFilePath, strDstFilePath, true);
        return true;
    }

    /**
     * Create directory if it not exist when giving a file or dir path.
     * 
     * @param string strPathName - The path file/dir name.
     * @param bool bIsDirPath - If the path name is file path or dir path, true is dir path, false is file path.
     * @return bool - true if success, otherwise false.
     */
    public bool CreateDirIfNotExist(string strPathName, bool bIsDirPath)
    {
        if (string.IsNullOrEmpty(strPathName))
        {
            return false;
        }

        if (Directory.Exists(strPathName))
        {
            return true;
        }

        string strFinalPathName = string.Empty;
        char chDirSpt = Path.DirectorySeparatorChar;
        if (PATH_SEPARATE_CHAR_SLASH == chDirSpt)
        {
            strFinalPathName = strPathName.Replace(PATH_SEPARATE_CHAR_BACKLASH, PATH_SEPARATE_CHAR_SLASH);
        }
        else if (PATH_SEPARATE_CHAR_BACKLASH == chDirSpt)
        {
            strFinalPathName = strPathName.Replace(PATH_SEPARATE_CHAR_SLASH, PATH_SEPARATE_CHAR_BACKLASH);
        }

        // Define the checking path.
        #if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        string strCheckPath = string.Empty;
        #else
        string strCheckPath = chDirSpt.ToString();
        #endif

        // Split the path.
        string[] aPathSegment = strFinalPathName.Split(new char[] {chDirSpt});
        int nSegmentCount = bIsDirPath ? aPathSegment.Length : (aPathSegment.Length - 1);
        for (int i = 0; i < nSegmentCount; i++)
        {
            if (string.IsNullOrEmpty(aPathSegment[i]))
            {
                continue;
            }
            
            strCheckPath += aPathSegment[i] + chDirSpt;
            if (!Directory.Exists(strCheckPath))
            {
                Directory.CreateDirectory(strCheckPath);
            }
        }

        return true;
    }
}

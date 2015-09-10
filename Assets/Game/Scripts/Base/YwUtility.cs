/**
 * The utility class.
 *
 * @filename  YwUtility.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-08-31
 */

using UnityEngine;
using System.IO;
using System.Collections;

// All utility here in this class.
public class YwUtility
{
    public static bool MakeDirectory(string strPath)
    {
        if (string.IsNullOrEmpty(strPath))
        {
            return false;
        }

        string strDir = Path.GetDirectoryName(strPath);
        if (!Directory.Exists(strDir))
        {
            Directory.CreateDirectory(strDir);
        }

        return true;
    }

    public static bool DeleteDirectory(string strPath)
    {
        if (string.IsNullOrEmpty(strPath))
        {
            return false;
        }

        string strDir = Path.GetDirectoryName(strPath);
        if (!Directory.Exists(strDir))
        {
            return false;
        }

        Directory.Delete(strDir, true);
        return true;
    }

    public static bool CopyFile(string strSrcFilePath, string strDstFilePath)
    {
        if (string.IsNullOrEmpty(strSrcFilePath) || string.IsNullOrEmpty(strDstFilePath))
        {
            return false;
        }

        if (!File.Exists(strSrcFilePath))
        {
            return false;
        }

        MakeDirectory(strDstFilePath);
        File.Copy(strSrcFilePath, strDstFilePath, true);

        return true;
    }
}

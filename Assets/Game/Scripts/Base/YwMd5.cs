/**
 * The md5 helper class.
 *
 * @filename  YwMd5.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-08-31
 */

using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Security.Cryptography;

// The md5 helper class.
public class YwMd5
{
    // The md5 instance.
    private MD5 m_cMd5 = null;

    // The conver text buffer.
    private StringBuilder m_cTextBuf = new StringBuilder();

    /**
     * Constructor.
     * 
     * @param void.
     * @return void.
     */
	public YwMd5()
    {
        m_cMd5 = MD5.Create();
    }

    /**
     * Destructor.
     * 
     * @param void.
     * @return void.
     */
    ~YwMd5()
    {
        Clear();
    }

    /**
     * Clear this helper class..
     * 
     * @param void.
     * @return void.
     */
    public void Clear()
    {
        if (null == m_cMd5)
        {
            return;
        }

        m_cMd5.Clear();
        m_cMd5 = null;
    }

    /**
     * Computer a md5 hash from file.
     * 
     * @param string strFilePath - The file path.
     * @return string - The md5 hash.
     */
    public string ComputeHash(string strFilePath)
    {
        if (string.IsNullOrEmpty(strFilePath))
        {
            return string.Empty;
        }

        YwArchiveBinFile cFileArc = new YwArchiveBinFile();
        if (!cFileArc.Open(strFilePath, FileMode.Open, FileAccess.Read))
        {
            return string.Empty;
        }

        if (!cFileArc.IsValid())
        {
            return string.Empty;
        }

        string strMd5Hash = ComputeHash(cFileArc.GetStream());
        cFileArc.Close();

        return strMd5Hash;
    }
	
    /**
     * Computer a md5 hash from a buffer.
     * 
     * @param byte[] aBuffer - The buffer.
     * @return string - The md5 hash.
     */
    public string ComputeHash(byte[] aBuffer)
    {
        if ((null == aBuffer) || (0 == aBuffer.Length))
        {
            return string.Empty;
        }

        if (null == m_cMd5)
        {
            YwDebug.LogError("You used an invalid md5 instance!");
            return string.Empty;
        }

        return GetMd5Hash(m_cMd5, aBuffer);
    }

    /**
     * Computer a md5 hash from a stream.
     * 
     * @param Stream cStream - The stream.
     * @return string - The md5 hash.
     */
    public string ComputeHash(Stream cStream)
    {
        if (null == cStream)
        {
            return string.Empty;
        }
        
        if (null == m_cMd5)
        {
            YwDebug.LogError("You used an invalid md5 instance!");
            return string.Empty;
        }
        
        return GetMd5Hash(m_cMd5, cStream);
    }

    /**
     * Computer a md5 hash from a buffer.
     * 
     * @param MD5 cMd5 - The md5 instance.
     * @param byte[] aBuffer - The input buffer.
     * @return string - The md5 hash.
     */
    private string GetMd5Hash(MD5 cMd5, byte[] aBuffer)
    {
        // Convert the input buffer to a byte array and compute the hash.
        byte[] aData = cMd5.ComputeHash(aBuffer);
        
        // Use a Stringbuilder to collect the bytes
        // and create a string.
        m_cTextBuf.Length = 0;
        
        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < aData.Length; i++)
        {
            m_cTextBuf.Append(aData[i].ToString("x2"));
        }
        
        // Return the hexadecimal string.
        return m_cTextBuf.ToString();
    }

    /**
     * Computer a md5 hash from a stream.
     * 
     * @param MD5 cMd5 - The md5 instance.
     * @param Stream cStream - The input stream.
     * @return string - The md5 hash.
     */
    private string GetMd5Hash(MD5 cMd5, Stream cStream)
    {
        // Convert the input stream to a byte array and compute the hash.
        byte[] aData = cMd5.ComputeHash(cStream);
        
        // Use a Stringbuilder to collect the bytes
        // and create a string.
        m_cTextBuf.Length = 0;
        
        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < aData.Length; i++)
        {
            m_cTextBuf.Append(aData[i].ToString("x2"));
        }
        
        // Return the hexadecimal string.
        return m_cTextBuf.ToString();
    }
}

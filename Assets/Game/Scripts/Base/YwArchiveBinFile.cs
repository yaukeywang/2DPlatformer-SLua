/**
 * Binary archive class with file stream.
 *
 * @filename  YwArchiveBinFile.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-08-23
 */

using System;
using System.IO;
using System.Text;

public class YwArchiveBinFile : YwArchiveBin
{
	/**
     * construct
     * @param void
     * @return void
     */
	public YwArchiveBinFile()
	{
	}
	
	/**
     * open file.
     * @param void.
     * @return void.
     */
	public bool Open(string strFileName, FileMode eMode, FileAccess eAccess)
	{
		if (string.IsNullOrEmpty(strFileName))
		{
			return false;
		}
		
		if ((FileMode.Open == eMode) && !File.Exists(strFileName))
		{
			return false;
		}
		
		try
		{
			m_cStream = new FileStream(strFileName, eMode, eAccess);
		}
		catch (Exception cEx)
		{
			Console.Write(cEx.Message);
		}
		
		if (null == m_cStream)
		{
			return false;
		}
		
		m_bOpen = true;
		return true;
	}
	
	/**
     * Close this stream.
     * @param void.
     * @return bool - true if success, otherwise false.
     */
	public bool Close()
	{
		if (null == m_cStream)
		{
			m_bOpen = false;
			return false;
		}
		
		m_cStream.Close();
		m_bOpen = false;
		return true;
	}
}

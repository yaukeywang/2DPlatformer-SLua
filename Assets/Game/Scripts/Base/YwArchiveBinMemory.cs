/**
 * Binary archive class with memory stream.
 *
 * @filename  YwArchiveBinMemory.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-08-23
 */

using System;
using System.IO;
using System.Text;

public class YwArchiveBinMemory : YwArchiveBin
{
	/**
     * Construct.
     * @param void.
     * @return void.
     */
	public YwArchiveBinMemory()
	{
	}
	
	/**
     * Open from a buffer.
     * @param byte[] aBuffer - The data buffer.
     * @return bool - true if success, otherwise false.
     */
	public bool Open(byte[] aBuffer)
	{
		if (null == aBuffer)
		{
			return false;
		}
		
		m_cStream = new MemoryStream(aBuffer);
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
     * @return void.
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

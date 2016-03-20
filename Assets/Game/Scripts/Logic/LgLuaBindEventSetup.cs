/**
 * The lua bind progress event setup class.
 *
 * @filename  LgLuaBindEventSetup.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   
 * @author    Yaukey
 * @date      2016-01-31
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// The lua bind progress event setup class.
public class LgLuaBindEventSetup : MonoBehaviour
{
	public Text m_luaBindProgressTips = null;
	private Slider m_cLuaBindProgressBar = null;

	// Use this for initialization
	void Awake()
	{
		m_cLuaBindProgressBar = gameObject.GetComponent<Slider>();
		if (null == m_cLuaBindProgressBar)
		{
			Debug.LogError("You add this script on a slider component!");
			return;
		}

		if (null == m_luaBindProgressTips)
		{
			Debug.LogError("You do not set lua binding progress tips!");
			return;
		}

		// Register event.
		YwLuaScriptMng.Instance.AddBindProgressEvent(BindProgress);
		m_cLuaBindProgressBar.onValueChanged.AddListener(UpdateBindProgressTips);
	}

	// Use this to destroy.
	void OnDestroy()
	{
		YwLuaScriptMng.Instance.RemoveBindProgressEvent(BindProgress);
	}

	/**
     * The lua bind progress event.
     * 
     * @param int nProgress - The current bind progress.
     * @return void.
     */
	private void BindProgress(int nProgress)
	{
		m_cLuaBindProgressBar.value = (float)nProgress;
	}

	/**
     * The lua bind progress tips.
     * 
     * @param float fProgress - The current bind progress.
     * @return void.
     */
	private void UpdateBindProgressTips(float fProgress)
	{
		m_luaBindProgressTips.text = "奋力加载中... " + (int)fProgress + "%";
	}
}

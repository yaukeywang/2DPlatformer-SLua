using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_LgLuaMonoDataBridge : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			LgLuaMonoDataBridge o;
			o=new LgLuaMonoDataBridge();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_sprites(IntPtr l) {
		try {
			LgLuaMonoDataBridge self=(LgLuaMonoDataBridge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_sprites);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_sprites(IntPtr l) {
		try {
			LgLuaMonoDataBridge self=(LgLuaMonoDataBridge)checkSelf(l);
			UnityEngine.Sprite[] v;
			checkArray(l,2,out v);
			self.m_sprites=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_audioClips(IntPtr l) {
		try {
			LgLuaMonoDataBridge self=(LgLuaMonoDataBridge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_audioClips);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_audioClips(IntPtr l) {
		try {
			LgLuaMonoDataBridge self=(LgLuaMonoDataBridge)checkSelf(l);
			UnityEngine.AudioClip[] v;
			checkArray(l,2,out v);
			self.m_audioClips=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"LgLuaMonoDataBridge");
		addMember(l,"m_sprites",get_m_sprites,set_m_sprites,true);
		addMember(l,"m_audioClips",get_m_audioClips,set_m_audioClips,true);
		createTypeMetatable(l,constructor, typeof(LgLuaMonoDataBridge),typeof(YwLuaMonoDataBridge));
	}
}

using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_YwLuaBehaviourExample : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetInstance(IntPtr l) {
		try {
			YwLuaBehaviourExample self=(YwLuaBehaviourExample)checkSelf(l);
			var ret=self.GetInstance();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_name(IntPtr l) {
		try {
			YwLuaBehaviourExample self=(YwLuaBehaviourExample)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_name);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_name(IntPtr l) {
		try {
			YwLuaBehaviourExample self=(YwLuaBehaviourExample)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.m_name=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"YwLuaBehaviourExample");
		addMember(l,GetInstance);
		addMember(l,"m_name",get_m_name,set_m_name,true);
		createTypeMetatable(l,null, typeof(YwLuaBehaviourExample),typeof(UnityEngine.MonoBehaviour));
	}
}

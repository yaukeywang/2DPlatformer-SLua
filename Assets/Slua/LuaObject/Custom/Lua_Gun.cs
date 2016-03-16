using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Gun : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_rocket(IntPtr l) {
		try {
			Gun self=(Gun)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_rocket);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_rocket(IntPtr l) {
		try {
			Gun self=(Gun)checkSelf(l);
			UnityEngine.Rigidbody2D v;
			checkType(l,2,out v);
			self.m_rocket=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_speed(IntPtr l) {
		try {
			Gun self=(Gun)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_speed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_speed(IntPtr l) {
		try {
			Gun self=(Gun)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_speed=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Gun");
		addMember(l,"m_rocket",get_m_rocket,set_m_rocket,true);
		addMember(l,"m_speed",get_m_speed,set_m_speed,true);
		createTypeMetatable(l,null, typeof(Gun),typeof(UnityEngine.MonoBehaviour));
	}
}

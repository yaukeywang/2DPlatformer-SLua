using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_YwLuaMonoDataBridge : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_integers(IntPtr l) {
		try {
			YwLuaMonoDataBridge self=(YwLuaMonoDataBridge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_integers);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_integers(IntPtr l) {
		try {
			YwLuaMonoDataBridge self=(YwLuaMonoDataBridge)checkSelf(l);
			System.Int32[] v;
			checkArray(l,2,out v);
			self.m_integers=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_floats(IntPtr l) {
		try {
			YwLuaMonoDataBridge self=(YwLuaMonoDataBridge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_floats);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_floats(IntPtr l) {
		try {
			YwLuaMonoDataBridge self=(YwLuaMonoDataBridge)checkSelf(l);
			System.Single[] v;
			checkArray(l,2,out v);
			self.m_floats=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_strings(IntPtr l) {
		try {
			YwLuaMonoDataBridge self=(YwLuaMonoDataBridge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_strings);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_strings(IntPtr l) {
		try {
			YwLuaMonoDataBridge self=(YwLuaMonoDataBridge)checkSelf(l);
			System.String[] v;
			checkArray(l,2,out v);
			self.m_strings=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"YwLuaMonoDataBridge");
		addMember(l,"m_integers",get_m_integers,set_m_integers,true);
		addMember(l,"m_floats",get_m_floats,set_m_floats,true);
		addMember(l,"m_strings",get_m_strings,set_m_strings,true);
		createTypeMetatable(l,null, typeof(YwLuaMonoDataBridge),typeof(UnityEngine.MonoBehaviour));
	}
}

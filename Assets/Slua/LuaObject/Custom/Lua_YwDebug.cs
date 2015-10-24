using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_YwDebug : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			YwDebug o;
			o=new YwDebug();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LogWarning_s(IntPtr l) {
		try {
			System.Object a1;
			checkType(l,1,out a1);
			YwDebug.LogWarning(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LogError_s(IntPtr l) {
		try {
			System.Object a1;
			checkType(l,1,out a1);
			YwDebug.LogError(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Log_s(IntPtr l) {
		try {
			System.Object a1;
			checkType(l,1,out a1);
			YwDebug.Log(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LogException_s(IntPtr l) {
		try {
			System.Exception a1;
			checkType(l,1,out a1);
			YwDebug.LogException(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"YwDebug");
		addMember(l,LogWarning_s);
		addMember(l,LogError_s);
		addMember(l,Log_s);
		addMember(l,LogException_s);
		createTypeMetatable(l,constructor, typeof(YwDebug));
	}
}

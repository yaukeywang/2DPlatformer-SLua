using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_YwLuaMonoBehaviour : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			YwLuaMonoBehaviour o;
			o=new YwLuaMonoBehaviour();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetLuaClassName(IntPtr l) {
		try {
			YwLuaMonoBehaviour self=(YwLuaMonoBehaviour)checkSelf(l);
			var ret=self.GetLuaClassName();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"YwLuaMonoBehaviour");
		addMember(l,GetLuaClassName);
		createTypeMetatable(l,constructor, typeof(YwLuaMonoBehaviour),typeof(YwLuaMonoBehaviourBase));
	}
}

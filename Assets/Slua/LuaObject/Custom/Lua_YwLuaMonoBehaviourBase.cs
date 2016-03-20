using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_YwLuaMonoBehaviourBase : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetLuaTable(IntPtr l) {
		try {
			YwLuaMonoBehaviourBase self=(YwLuaMonoBehaviourBase)checkSelf(l);
			var ret=self.GetLuaTable();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"YwLuaMonoBehaviourBase");
		addMember(l,GetLuaTable);
		createTypeMetatable(l,null, typeof(YwLuaMonoBehaviourBase),typeof(UnityEngine.MonoBehaviour));
	}
}

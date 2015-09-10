using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Score : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_score(IntPtr l) {
		try {
			Score self=(Score)checkSelf(l);
			pushValue(l,self.m_score);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_score(IntPtr l) {
		try {
			Score self=(Score)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.m_score=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Score");
		addMember(l,"m_score",get_m_score,set_m_score,true);
		createTypeMetatable(l,null, typeof(Score),typeof(UnityEngine.MonoBehaviour));
	}
}

using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_LayBombs : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_bombCount(IntPtr l) {
		try {
			LayBombs self=(LayBombs)checkSelf(l);
			pushValue(l,self.m_bombCount);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_bombCount(IntPtr l) {
		try {
			LayBombs self=(LayBombs)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.m_bombCount=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_bombsAway(IntPtr l) {
		try {
			LayBombs self=(LayBombs)checkSelf(l);
			pushValue(l,self.m_bombsAway);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_bombsAway(IntPtr l) {
		try {
			LayBombs self=(LayBombs)checkSelf(l);
			UnityEngine.AudioClip v;
			checkType(l,2,out v);
			self.m_bombsAway=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_bomb(IntPtr l) {
		try {
			LayBombs self=(LayBombs)checkSelf(l);
			pushValue(l,self.m_bomb);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_bomb(IntPtr l) {
		try {
			LayBombs self=(LayBombs)checkSelf(l);
			UnityEngine.GameObject v;
			checkType(l,2,out v);
			self.m_bomb=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_BombCount(IntPtr l) {
		try {
			LayBombs self=(LayBombs)checkSelf(l);
			pushValue(l,self.BombCount);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_BombCount(IntPtr l) {
		try {
			LayBombs self=(LayBombs)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.BombCount=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_BombLaid(IntPtr l) {
		try {
			LayBombs self=(LayBombs)checkSelf(l);
			pushValue(l,self.BombLaid);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_BombLaid(IntPtr l) {
		try {
			LayBombs self=(LayBombs)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.BombLaid=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"LayBombs");
		addMember(l,"m_bombCount",get_m_bombCount,set_m_bombCount,true);
		addMember(l,"m_bombsAway",get_m_bombsAway,set_m_bombsAway,true);
		addMember(l,"m_bomb",get_m_bomb,set_m_bomb,true);
		addMember(l,"BombCount",get_BombCount,set_BombCount,true);
		addMember(l,"BombLaid",get_BombLaid,set_BombLaid,true);
		createTypeMetatable(l,null, typeof(LayBombs),typeof(UnityEngine.MonoBehaviour));
	}
}

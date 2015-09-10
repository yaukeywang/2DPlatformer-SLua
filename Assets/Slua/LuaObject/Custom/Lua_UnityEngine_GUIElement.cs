using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_GUIElement : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.GUIElement o;
			o=new UnityEngine.GUIElement();
			pushValue(l,o);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int HitTest(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				UnityEngine.GUIElement self=(UnityEngine.GUIElement)checkSelf(l);
				UnityEngine.Vector3 a1;
				checkType(l,2,out a1);
				var ret=self.HitTest(a1);
				pushValue(l,ret);
				return 1;
			}
			else if(argc==3){
				UnityEngine.GUIElement self=(UnityEngine.GUIElement)checkSelf(l);
				UnityEngine.Vector3 a1;
				checkType(l,2,out a1);
				UnityEngine.Camera a2;
				checkType(l,3,out a2);
				var ret=self.HitTest(a1,a2);
				pushValue(l,ret);
				return 1;
			}
			LuaDLL.luaL_error(l,"No matched override function to call");
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetScreenRect(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				UnityEngine.GUIElement self=(UnityEngine.GUIElement)checkSelf(l);
				var ret=self.GetScreenRect();
				pushValue(l,ret);
				return 1;
			}
			else if(argc==2){
				UnityEngine.GUIElement self=(UnityEngine.GUIElement)checkSelf(l);
				UnityEngine.Camera a1;
				checkType(l,2,out a1);
				var ret=self.GetScreenRect(a1);
				pushValue(l,ret);
				return 1;
			}
			LuaDLL.luaL_error(l,"No matched override function to call");
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.GUIElement");
		addMember(l,HitTest);
		addMember(l,GetScreenRect);
		createTypeMetatable(l,constructor, typeof(UnityEngine.GUIElement),typeof(UnityEngine.Behaviour));
	}
}

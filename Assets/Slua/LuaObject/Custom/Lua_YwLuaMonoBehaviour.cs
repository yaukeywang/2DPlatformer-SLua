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
	static public int get_m_className(IntPtr l) {
		try {
			YwLuaMonoBehaviour self=(YwLuaMonoBehaviour)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_className);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_className(IntPtr l) {
		try {
			YwLuaMonoBehaviour self=(YwLuaMonoBehaviour)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.m_className=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_monoMethods(IntPtr l) {
		try {
			YwLuaMonoBehaviour self=(YwLuaMonoBehaviour)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_monoMethods);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_monoMethods(IntPtr l) {
		try {
			YwLuaMonoBehaviour self=(YwLuaMonoBehaviour)checkSelf(l);
			System.Collections.Generic.List<YwLuaMonoBehaviour.EMonoMethod> v;
			checkType(l,2,out v);
			self.m_monoMethods=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_parameters(IntPtr l) {
		try {
			YwLuaMonoBehaviour self=(YwLuaMonoBehaviour)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_parameters);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_parameters(IntPtr l) {
		try {
			YwLuaMonoBehaviour self=(YwLuaMonoBehaviour)checkSelf(l);
			UnityEngine.GameObject[] v;
			checkArray(l,2,out v);
			self.m_parameters=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"YwLuaMonoBehaviour");
		addMember(l,"m_className",get_m_className,set_m_className,true);
		addMember(l,"m_monoMethods",get_m_monoMethods,set_m_monoMethods,true);
		addMember(l,"m_parameters",get_m_parameters,set_m_parameters,true);
		createTypeMetatable(l,constructor, typeof(YwLuaMonoBehaviour),typeof(YwLuaMonoBehaviourBase));
	}
}

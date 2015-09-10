using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_PlayerHealth : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdateHealthBar(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			self.UpdateHealthBar();
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_health(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			pushValue(l,self.m_health);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_health(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_health=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_repeatDamagePeriod(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			pushValue(l,self.m_repeatDamagePeriod);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_repeatDamagePeriod(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_repeatDamagePeriod=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_ouchClips(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			pushValue(l,self.m_ouchClips);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_ouchClips(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			UnityEngine.AudioClip[] v;
			checkType(l,2,out v);
			self.m_ouchClips=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_hurtForce(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			pushValue(l,self.m_hurtForce);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_hurtForce(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_hurtForce=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_damageAmount(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			pushValue(l,self.m_damageAmount);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_damageAmount(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_damageAmount=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"PlayerHealth");
		addMember(l,UpdateHealthBar);
		addMember(l,"m_health",get_m_health,set_m_health,true);
		addMember(l,"m_repeatDamagePeriod",get_m_repeatDamagePeriod,set_m_repeatDamagePeriod,true);
		addMember(l,"m_ouchClips",get_m_ouchClips,set_m_ouchClips,true);
		addMember(l,"m_hurtForce",get_m_hurtForce,set_m_hurtForce,true);
		addMember(l,"m_damageAmount",get_m_damageAmount,set_m_damageAmount,true);
		createTypeMetatable(l,null, typeof(PlayerHealth),typeof(UnityEngine.MonoBehaviour));
	}
}

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
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_health(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_health);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_health(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_health=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_repeatDamagePeriod(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_repeatDamagePeriod);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_repeatDamagePeriod(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_repeatDamagePeriod=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_ouchClips(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_ouchClips);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_ouchClips(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			UnityEngine.AudioClip[] v;
			checkArray(l,2,out v);
			self.m_ouchClips=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_hurtForce(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_hurtForce);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_hurtForce(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_hurtForce=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_damageAmount(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_damageAmount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_damageAmount(IntPtr l) {
		try {
			PlayerHealth self=(PlayerHealth)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_damageAmount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
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

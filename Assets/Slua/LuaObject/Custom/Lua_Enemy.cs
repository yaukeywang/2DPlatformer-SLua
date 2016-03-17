using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Enemy : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Hurt(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			self.Hurt();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_moveSpeed(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_moveSpeed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_moveSpeed(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_moveSpeed=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_HP(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_HP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_HP(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.m_HP=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_deadEnemy(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_deadEnemy);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_deadEnemy(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			UnityEngine.Sprite v;
			checkType(l,2,out v);
			self.m_deadEnemy=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_damagedEnemy(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_damagedEnemy);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_damagedEnemy(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			UnityEngine.Sprite v;
			checkType(l,2,out v);
			self.m_damagedEnemy=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_deathClips(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_deathClips);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_deathClips(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			UnityEngine.AudioClip[] v;
			checkArray(l,2,out v);
			self.m_deathClips=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_hundredPointsUI(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_hundredPointsUI);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_hundredPointsUI(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			UnityEngine.GameObject v;
			checkType(l,2,out v);
			self.m_hundredPointsUI=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_deathSpinMin(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_deathSpinMin);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_deathSpinMin(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_deathSpinMin=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_deathSpinMax(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_deathSpinMax);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_deathSpinMax(IntPtr l) {
		try {
			Enemy self=(Enemy)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_deathSpinMax=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Enemy");
		addMember(l,Hurt);
		addMember(l,"m_moveSpeed",get_m_moveSpeed,set_m_moveSpeed,true);
		addMember(l,"m_HP",get_m_HP,set_m_HP,true);
		addMember(l,"m_deadEnemy",get_m_deadEnemy,set_m_deadEnemy,true);
		addMember(l,"m_damagedEnemy",get_m_damagedEnemy,set_m_damagedEnemy,true);
		addMember(l,"m_deathClips",get_m_deathClips,set_m_deathClips,true);
		addMember(l,"m_hundredPointsUI",get_m_hundredPointsUI,set_m_hundredPointsUI,true);
		addMember(l,"m_deathSpinMin",get_m_deathSpinMin,set_m_deathSpinMin,true);
		addMember(l,"m_deathSpinMax",get_m_deathSpinMax,set_m_deathSpinMax,true);
		createTypeMetatable(l,null, typeof(Enemy),typeof(UnityEngine.MonoBehaviour));
	}
}

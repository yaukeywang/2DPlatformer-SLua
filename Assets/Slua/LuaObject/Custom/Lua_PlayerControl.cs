using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_PlayerControl : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Taunt(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			self.Taunt();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_bFacingRight(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_bFacingRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_bFacingRight(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.m_bFacingRight=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_bJump(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_bJump);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_bJump(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.m_bJump=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_moveForce(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_moveForce);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_moveForce(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_moveForce=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_maxSpeed(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_maxSpeed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_maxSpeed(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_maxSpeed=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_jumpClips(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_jumpClips);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_jumpClips(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			UnityEngine.AudioClip[] v;
			checkArray(l,2,out v);
			self.m_jumpClips=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_jumpForce(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_jumpForce);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_jumpForce(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_jumpForce=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_taunts(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_taunts);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_taunts(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			UnityEngine.AudioClip[] v;
			checkArray(l,2,out v);
			self.m_taunts=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_tauntProbability(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_tauntProbability);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_tauntProbability(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_tauntProbability=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_tauntDelay(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_tauntDelay);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_tauntDelay(IntPtr l) {
		try {
			PlayerControl self=(PlayerControl)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_tauntDelay=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"PlayerControl");
		addMember(l,Taunt);
		addMember(l,"m_bFacingRight",get_m_bFacingRight,set_m_bFacingRight,true);
		addMember(l,"m_bJump",get_m_bJump,set_m_bJump,true);
		addMember(l,"m_moveForce",get_m_moveForce,set_m_moveForce,true);
		addMember(l,"m_maxSpeed",get_m_maxSpeed,set_m_maxSpeed,true);
		addMember(l,"m_jumpClips",get_m_jumpClips,set_m_jumpClips,true);
		addMember(l,"m_jumpForce",get_m_jumpForce,set_m_jumpForce,true);
		addMember(l,"m_taunts",get_m_taunts,set_m_taunts,true);
		addMember(l,"m_tauntProbability",get_m_tauntProbability,set_m_tauntProbability,true);
		addMember(l,"m_tauntDelay",get_m_tauntDelay,set_m_tauntDelay,true);
		createTypeMetatable(l,null, typeof(PlayerControl),typeof(UnityEngine.MonoBehaviour));
	}
}

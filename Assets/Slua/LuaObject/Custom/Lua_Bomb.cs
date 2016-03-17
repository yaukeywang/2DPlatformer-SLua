using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Bomb : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Explode(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			self.Explode();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_bombRadius(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_bombRadius);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_bombRadius(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_bombRadius=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_bombForce(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_bombForce);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_bombForce(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_bombForce=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_boom(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_boom);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_boom(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			UnityEngine.AudioClip v;
			checkType(l,2,out v);
			self.m_boom=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_fuse(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_fuse);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_fuse(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			UnityEngine.AudioClip v;
			checkType(l,2,out v);
			self.m_fuse=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_fuseTime(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_fuseTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_fuseTime(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_fuseTime=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_explosion(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_explosion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_explosion(IntPtr l) {
		try {
			Bomb self=(Bomb)checkSelf(l);
			UnityEngine.GameObject v;
			checkType(l,2,out v);
			self.m_explosion=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Bomb");
		addMember(l,Explode);
		addMember(l,"m_bombRadius",get_m_bombRadius,set_m_bombRadius,true);
		addMember(l,"m_bombForce",get_m_bombForce,set_m_bombForce,true);
		addMember(l,"m_boom",get_m_boom,set_m_boom,true);
		addMember(l,"m_fuse",get_m_fuse,set_m_fuse,true);
		addMember(l,"m_fuseTime",get_m_fuseTime,set_m_fuseTime,true);
		addMember(l,"m_explosion",get_m_explosion,set_m_explosion,true);
		createTypeMetatable(l,null, typeof(Bomb),typeof(UnityEngine.MonoBehaviour));
	}
}

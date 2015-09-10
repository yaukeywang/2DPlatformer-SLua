using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_CameraFollow : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_xMargin(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,self.m_xMargin);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_xMargin(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_xMargin=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_yMargin(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,self.m_yMargin);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_yMargin(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_yMargin=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_xSmooth(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,self.m_xSmooth);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_xSmooth(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_xSmooth=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_ySmooth(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,self.m_ySmooth);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_ySmooth(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_ySmooth=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_maxXAndY(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,self.m_maxXAndY);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_maxXAndY(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.m_maxXAndY=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_minXAndY(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,self.m_minXAndY);
			return 1;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_minXAndY(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.m_minXAndY=v;
			return 0;
		}
		catch(Exception e) {
			LuaDLL.luaL_error(l, e.ToString());
			return 0;
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"CameraFollow");
		addMember(l,"m_xMargin",get_m_xMargin,set_m_xMargin,true);
		addMember(l,"m_yMargin",get_m_yMargin,set_m_yMargin,true);
		addMember(l,"m_xSmooth",get_m_xSmooth,set_m_xSmooth,true);
		addMember(l,"m_ySmooth",get_m_ySmooth,set_m_ySmooth,true);
		addMember(l,"m_maxXAndY",get_m_maxXAndY,set_m_maxXAndY,true);
		addMember(l,"m_minXAndY",get_m_minXAndY,set_m_minXAndY,true);
		createTypeMetatable(l,null, typeof(CameraFollow),typeof(UnityEngine.MonoBehaviour));
	}
}

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
			pushValue(l,true);
			pushValue(l,self.m_xMargin);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_xMargin(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_xMargin=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_yMargin(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_yMargin);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_yMargin(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_yMargin=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_xSmooth(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_xSmooth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_xSmooth(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_xSmooth=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_ySmooth(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_ySmooth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_ySmooth(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_ySmooth=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_maxXAndY(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_maxXAndY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_maxXAndY(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.m_maxXAndY=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_minXAndY(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_minXAndY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_minXAndY(IntPtr l) {
		try {
			CameraFollow self=(CameraFollow)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.m_minXAndY=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
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

using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_GUITexture : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.GUITexture o;
			o=new UnityEngine.GUITexture();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_color(IntPtr l) {
		try {
			UnityEngine.GUITexture self=(UnityEngine.GUITexture)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.color);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_color(IntPtr l) {
		try {
			UnityEngine.GUITexture self=(UnityEngine.GUITexture)checkSelf(l);
			UnityEngine.Color v;
			checkType(l,2,out v);
			self.color=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_texture(IntPtr l) {
		try {
			UnityEngine.GUITexture self=(UnityEngine.GUITexture)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.texture);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_texture(IntPtr l) {
		try {
			UnityEngine.GUITexture self=(UnityEngine.GUITexture)checkSelf(l);
			UnityEngine.Texture v;
			checkType(l,2,out v);
			self.texture=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_pixelInset(IntPtr l) {
		try {
			UnityEngine.GUITexture self=(UnityEngine.GUITexture)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.pixelInset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_pixelInset(IntPtr l) {
		try {
			UnityEngine.GUITexture self=(UnityEngine.GUITexture)checkSelf(l);
			UnityEngine.Rect v;
			checkValueType(l,2,out v);
			self.pixelInset=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_border(IntPtr l) {
		try {
			UnityEngine.GUITexture self=(UnityEngine.GUITexture)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.border);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_border(IntPtr l) {
		try {
			UnityEngine.GUITexture self=(UnityEngine.GUITexture)checkSelf(l);
			UnityEngine.RectOffset v;
			checkType(l,2,out v);
			self.border=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.GUITexture");
		addMember(l,"color",get_color,set_color,true);
		addMember(l,"texture",get_texture,set_texture,true);
		addMember(l,"pixelInset",get_pixelInset,set_pixelInset,true);
		addMember(l,"border",get_border,set_border,true);
		createTypeMetatable(l,constructor, typeof(UnityEngine.GUITexture),typeof(UnityEngine.GUIElement));
	}
}

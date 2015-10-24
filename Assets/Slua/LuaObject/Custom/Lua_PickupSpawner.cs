using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_PickupSpawner : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int DeliverPickup(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			self.DeliverPickup();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_pickups(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_pickups);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_pickups(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			UnityEngine.GameObject[] v;
			checkType(l,2,out v);
			self.m_pickups=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_pickupDeliveryTime(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_pickupDeliveryTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_pickupDeliveryTime(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_pickupDeliveryTime=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_dropRangeLeft(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_dropRangeLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_dropRangeLeft(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_dropRangeLeft=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_dropRangeRight(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_dropRangeRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_dropRangeRight(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_dropRangeRight=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_highHealthThreshold(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_highHealthThreshold);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_highHealthThreshold(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_highHealthThreshold=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_lowHealthThreshold(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_lowHealthThreshold);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_lowHealthThreshold(IntPtr l) {
		try {
			PickupSpawner self=(PickupSpawner)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.m_lowHealthThreshold=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"PickupSpawner");
		addMember(l,DeliverPickup);
		addMember(l,"m_pickups",get_m_pickups,set_m_pickups,true);
		addMember(l,"m_pickupDeliveryTime",get_m_pickupDeliveryTime,set_m_pickupDeliveryTime,true);
		addMember(l,"m_dropRangeLeft",get_m_dropRangeLeft,set_m_dropRangeLeft,true);
		addMember(l,"m_dropRangeRight",get_m_dropRangeRight,set_m_dropRangeRight,true);
		addMember(l,"m_highHealthThreshold",get_m_highHealthThreshold,set_m_highHealthThreshold,true);
		addMember(l,"m_lowHealthThreshold",get_m_lowHealthThreshold,set_m_lowHealthThreshold,true);
		createTypeMetatable(l,null, typeof(PickupSpawner),typeof(UnityEngine.MonoBehaviour));
	}
}

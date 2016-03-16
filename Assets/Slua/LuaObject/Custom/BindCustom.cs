using System;
using System.Collections.Generic;
namespace SLua {
	[LuaBinder(3)]
	public class BindCustom {
		public static Action<IntPtr>[] GetBindList() {
			Action<IntPtr>[] list= {
				Lua_YwDebug.reg,
				Lua_YwLuaBehaviourExample.reg,
				Lua_Bomb.reg,
				Lua_CameraFollow.reg,
				Lua_Enemy.reg,
				Lua_Gun.reg,
				Lua_LayBombs.reg,
				Lua_PickupSpawner.reg,
				Lua_PlayerControl.reg,
				Lua_PlayerHealth.reg,
				Lua_Score.reg,
				Lua_Custom.reg,
				Lua_Deleg.reg,
				Lua_foostruct.reg,
				Lua_FloatEvent.reg,
				Lua_ListViewEvent.reg,
				Lua_SLuaTest.reg,
				Lua_System_Collections_Generic_List_1_int.reg,
				Lua_XXList.reg,
				Lua_AbsClass.reg,
				Lua_HelloWorld.reg,
				Lua_System_Collections_Generic_Dictionary_2_int_string.reg,
				Lua_System_String.reg,
				Lua_UnityEngine_GUIElement.reg,
				Lua_UnityEngine_GUIText.reg,
				Lua_UnityEngine_GUITexture.reg,
			};
			return list;
		}
	}
}

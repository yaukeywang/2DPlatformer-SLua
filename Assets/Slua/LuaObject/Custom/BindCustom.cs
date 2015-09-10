using System;
namespace SLua {
	[LuaBinder(3)]
	public class BindCustom {
		public static void Bind(IntPtr l) {
			Lua_YwDebug.reg(l);
			Lua_YwLuaBehaviourExample.reg(l);
			Lua_Bomb.reg(l);
			Lua_CameraFollow.reg(l);
			Lua_Enemy.reg(l);
			Lua_Gun.reg(l);
			Lua_LayBombs.reg(l);
			Lua_PickupSpawner.reg(l);
			Lua_PlayerControl.reg(l);
			Lua_PlayerHealth.reg(l);
			Lua_Score.reg(l);
			Lua_Custom.reg(l);
			Lua_Deleg.reg(l);
			Lua_foostruct.reg(l);
			Lua_SLuaTest.reg(l);
			Lua_System_Collections_Generic_List_1_int.reg(l);
			Lua_XXList.reg(l);
			Lua_HelloWorld.reg(l);
			Lua_System_Collections_Generic_Dictionary_2_int_string.reg(l);
			Lua_System_String.reg(l);
			Lua_UnityEngine_GUIElement.reg(l);
			Lua_UnityEngine_GUIText.reg(l);
			Lua_UnityEngine_GUITexture.reg(l);
		}
	}
}

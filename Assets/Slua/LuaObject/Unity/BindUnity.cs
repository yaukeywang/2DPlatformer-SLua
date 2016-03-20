using System;
using System.Collections.Generic;
namespace SLua {
	[LuaBinder(0)]
	public class BindUnity {
		public static Action<IntPtr>[] GetBindList() {
			Action<IntPtr>[] list= {
				Lua_UnityEngine_WaitForSeconds.reg,
				Lua_UnityEngine_Object.reg,
				Lua_UnityEngine_Component.reg,
				Lua_UnityEngine_Renderer.reg,
				Lua_UnityEngine_Behaviour.reg,
				Lua_UnityEngine_GUIElement.reg,
				Lua_UnityEngine_GUITexture.reg,
				Lua_UnityEngine_LayerMask.reg,
				Lua_UnityEngine_Vector2.reg,
				Lua_UnityEngine_Vector3.reg,
				Lua_UnityEngine_Quaternion.reg,
				Lua_UnityEngine_Vector4.reg,
				Lua_UnityEngine_Mathf.reg,
				Lua_UnityEngine_Material.reg,
				Lua_UnityEngine_Sprite.reg,
				Lua_UnityEngine_SpriteRenderer.reg,
				Lua_UnityEngine_Application.reg,
				Lua_UnityEngine_Camera.reg,
				Lua_UnityEngine_Debug.reg,
				Lua_UnityEngine_MonoBehaviour.reg,
				Lua_UnityEngine_Input.reg,
				Lua_UnityEngine_GameObject.reg,
				Lua_UnityEngine_Transform.reg,
				Lua_UnityEngine_Time.reg,
				Lua_UnityEngine_Random.reg,
				Lua_UnityEngine_SceneManagement_SceneManager.reg,
				Lua_UnityEngine_SceneManagement_Scene.reg,
				Lua_UnityEngine_ParticleSystem.reg,
				Lua_UnityEngine_ParticleSystemRenderer.reg,
				Lua_UnityEngine_Physics2D.reg,
				Lua_UnityEngine_RaycastHit2D.reg,
				Lua_UnityEngine_RigidbodyConstraints2D.reg,
				Lua_UnityEngine_Rigidbody2D.reg,
				Lua_UnityEngine_Collider2D.reg,
				Lua_UnityEngine_CircleCollider2D.reg,
				Lua_UnityEngine_BoxCollider2D.reg,
				Lua_UnityEngine_EdgeCollider2D.reg,
				Lua_UnityEngine_PolygonCollider2D.reg,
				Lua_UnityEngine_Collision2D.reg,
				Lua_UnityEngine_AudioClip.reg,
				Lua_UnityEngine_AudioSource.reg,
				Lua_UnityEngine_Animator.reg,
				Lua_UnityEngine_GUIText.reg,
				Lua_UnityEngine_KeyCode.reg,
				Lua_UnityEngine_Color.reg,
			};
			return list;
		}
	}
}

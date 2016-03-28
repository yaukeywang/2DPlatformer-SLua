/**
 * The custom export lua type class, this is editor class.
 *
 * @filename  LgCustomExportTypeToLua.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2016-03-16
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SLua;

// The custom export lua type class.
public class LgCustomExportTypeToLua : ICustomExportPost
{
    /**
     * Get custom assembly to generate extension method.
     * 
     * @param out List<string> cList - The result out list.
     * @return void.
     */
    public static void OnGetAssemblyToGenerateExtensionMethod(out List<string> cList)
    {
        cList = new List<string>
        {
            
        };
    }

    /**
     * Add custom class type to export.
     * 
     * @param LuaCodeGen.ExportGenericDelegate cAdd - The type adder delegate.
     * @return void.
     */
    public static void OnAddCustomClass(LuaCodeGen.ExportGenericDelegate cAdd)
    {
        // add your custom class here
        // add( type, typename)
        // type is what you want to export
        // typename used for simplify generic type name or rename, like List<int> named to "ListInt", if not a generic type keep typename as null or rename as new type name.
        //cAdd(typeof(string), "String");

        cAdd(typeof(YwDebug), null);
        cAdd(typeof(YwLuaMonoBehaviourBase), null);
        cAdd(typeof(YwLuaMonoBehaviour), null);
        cAdd(typeof(YwLuaMonoDataBridge), null);
        cAdd(typeof(LgLuaMonoDataBridge), null);
    }

    /**
     * Add custom assembly type to export.
     * 
     * @param ref List<string> cList - The name list of custom assembly to load.
     * @return void.
     */
    public static void OnAddCustomAssembly(ref List<string> cList)
    {
        // add your custom assembly here
        // you can build a dll for 3rd library like ngui titled assembly name "NGUI", put it in Assets folder
        // add its name into list, slua will generate all exported interface automatically for you.
        //cList.Add("NGUI");
    }

    /**
     * Get custom namespace to export.
     * 
     * @param void.
     * @return HashSet<string> - The set of the string.
     */
    public static HashSet<string> OnAddCustomNamespace()
    {
        return new HashSet<string>
        {
            
        };
    }

    // The filter list.
    public static List<string> FunctionFilterList = new List<string>()
    {
        
    };

    /**
     * Get the use list for exporting unity engine.
     * 
     * @param out List<string> list - The name of exporting list.
     * @return void.
     */
    public static void OnGetUseList(out List<string> list)
    {
        list = new List<string>
        {
            "UnityEngine.Object",
            "UnityEngine.GameObject",
            "UnityEngine.Component",
            "UnityEngine.Behaviour",
            "UnityEngine.MonoBehaviour",
            "UnityEngine.Transform",
            "UnityEngine.Camera",
            "UnityEngine.GUIElement",
            "UnityEngine.GUIText",
            "UnityEngine.GUITexture",
            "UnityEngine.Animator",
            "UnityEngine.Color",
            "UnityEngine.Material",
            "UnityEngine.Renderer",
            "UnityEngine.SpriteRenderer",
            "UnityEngine.ParticleSystemRenderer",
            "UnityEngine.ParticleSystem",
            "UnityEngine.Sprite",
            "UnityEngine.AudioSource",
            "UnityEngine.AudioClip",
            "UnityEngine.Time",
            "UnityEngine.WaitForSeconds",
            "UnityEngine.Vector2",
            "UnityEngine.Vector3",
            "UnityEngine.Vector4",
            "UnityEngine.Quaternion",
            "UnityEngine.Physics2D",
            "UnityEngine.RaycastHit2D",
            "UnityEngine.Rigidbody2D",
            "UnityEngine.RigidbodyConstraints2D",
            "UnityEngine.Collision2D",
            "UnityEngine.Collider2D",
            "UnityEngine.BoxCollider2D",
            "UnityEngine.CircleCollider2D",
            "UnityEngine.PolygonCollider2D",
            "UnityEngine.EdgeCollider2D",
            "UnityEngine.Input",
            "UnityEngine.KeyCode",
            "UnityEngine.SceneManagement",
            "UnityEngine.SceneManagement.SceneManager",
            "UnityEngine.SceneManagement.Scene",
            "UnityEngine.Debug",
            "UnityEngine.LayerMask",
            "UnityEngine.Mathf",
            "UnityEngine.Random",
            "UnityEngine.Application",
        };
    }

    /**
     * Get the useless list for exporting unity engine.
     * 
     * @param out List<string> list - The name of useless list.
     * @return void.
     */
    public static void OnGetNoUseList(out List<string> list)
    {
        list = new List<string>
        {
            
        };
    }
}

/**
 * The custom export lua type class, this is editor class.
 *
 * @filename  CustomExportTypeToLua.cs
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
public class CustomExportTypeToLua : ICustomExportPost
{
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

        cAdd(typeof(GUIElement), null);
        cAdd(typeof(GUIText), null);
        cAdd(typeof(GUITexture), null);
        cAdd(typeof(YwDebug), null);
        cAdd(typeof(YwLuaMonoBehaviourBase), null);
        cAdd(typeof(YwLuaMonoDataBridge), null);
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
}

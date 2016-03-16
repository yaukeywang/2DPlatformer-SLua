using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SLua;

public class CustomExportTypeToLua : ICustomExportPost
{

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
    }

    public static void OnAddCustomAssembly(ref List<string> cList)
    {
        // add your custom assembly here
        // you can build a dll for 3rd library like ngui titled assembly name "NGUI", put it in Assets folder
        // add its name into list, slua will generate all exported interface automatically for you
        //cList.Add("NGUI");
    }
}

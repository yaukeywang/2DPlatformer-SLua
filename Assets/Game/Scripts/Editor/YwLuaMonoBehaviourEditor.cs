/**
 * The custom editor of the YwLuaMonoBehaviourEditor class.
 *
 * @filename  YwLuaMonoBehaviourEditor.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2016-03-25
 */

using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

// The custom editor of the YwLuaMonoBehaviourEditor class.
[CustomEditor(typeof(YwLuaMonoBehaviour), true)]
public class YwLuaMonoBehaviourEditor : Editor
{
    // The lua params property.
    protected SerializedProperty m_cLuaParams = null;

    /**
     * This function is called when the object is loaded.
     * 
     * @param void.
     * @return void.
     */
    protected virtual void OnEnable()
    {
        m_cLuaParams = serializedObject.FindProperty("m_luaParameters");
    }

    /**
     * Implement this function to make a custom inspector.
     * Inside this function you can add your own custom GUI for the inspector of a specific object class.
     * 
     * @param void.
     * @return void.
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_cLuaParams);
        serializedObject.ApplyModifiedProperties();
    }
}

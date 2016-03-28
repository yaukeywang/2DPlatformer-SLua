/**
 * The property drawer of the YwLuaMonoBehaviourParamsPropertyDrawer class.
 *
 * @filename  YwLuaMonoBehaviourParamsPropertyDrawer.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2016-03-25
 */

using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

// The property drawer of the YwLuaMonoBehaviourParamsPropertyDrawer class.
[CustomPropertyDrawer(typeof(YwLuaMonoBehaviourParams))]
public class YwLuaMonoBehaviourParamsPropertyDrawer : PropertyDrawer
{
    // The param of the contex menu for mono methods selection.
    protected struct MethodMenuEventParam
    {
        // The serialized property.
        public readonly SerializedProperty m_cListener;

        // The enum value of the event (YwLuaMonoBehaviourParams.EMonoMethod).
        public readonly int m_nEventIdx;

        // Constructor.
        public MethodMenuEventParam(SerializedProperty cListener, int nEventIdx)
        {
            m_cListener = cListener;
            m_nEventIdx = nEventIdx;
        }
    }

    // The built-in icon resources.
    protected static readonly GUIContent m_cIconToolbarPlus = EditorGUIUtility.IconContent("Toolbar Plus", "Add to list.");
    protected static readonly GUIContent m_cIconToolbarPlusMore = EditorGUIUtility.IconContent("Toolbar Plus More", "Choose to add to list.");
    protected static readonly GUIContent m_cIconToolbarMinus = EditorGUIUtility.IconContent("Toolbar Minus", "Remove selection from list.");

    // The name content.
    protected static readonly GUIContent m_cLabelClassName = new GUIContent("Lua Class Name");

    // The built-in gui style.
    protected static readonly GUIStyle m_cGsHeaderBackground = "RL Header";
    protected static readonly GUIStyle m_cGsBoxBackground = "RL Background";
    protected static readonly GUIStyle m_cGsFooterBackground = "RL Footer";
    protected static readonly GUIStyle m_cGsPreButton = "RL FooterButton";
    protected static readonly GUIStyle m_cGsElementBackground = "RL Element";
    protected static readonly GUIStyle m_cGsElementBackground2 = "OL Box";
    protected static readonly GUIStyle m_cGsInvisibleRemoveButton = "InvisibleButton";

    // The constant value for gui area.
    protected static readonly float m_fSingleLineHeight = EditorGUIUtility.singleLineHeight;
    protected static readonly float m_fSingleLineGap = 3.0f;
    protected static readonly float m_fSingleLineGapEx = 4.0f;
    protected static readonly float m_fFooterROffset = 8.0f;
    protected static readonly float m_fFooterROffsetEx = 4.0f;
    protected static readonly float m_fFooterBtnROffset = 25.0f;
    protected static readonly float m_fFooterBtnWidth = 25.0f;
    protected static readonly float m_fFooterBtnHeight = 13.0f;
    protected static readonly float m_fElementLROffset = 6.0f;
    protected static readonly float m_fElementLROffsetEx = 2.0f;
    protected static readonly float m_fElementLROffsetEx2 = 1.0f;
    protected static readonly float m_fElementLROffsetEx3 = 4.0f;
    protected static readonly float m_fElementRemoveBtnSize = 16.0f;
    protected static readonly float m_fElementRemoveBtnLROffset = 22.0f;

    // Current control id.
    private int m_nControlID = -1;

    // Constructor.
    public YwLuaMonoBehaviourParamsPropertyDrawer() : base()
    {
        m_nControlID = GUIUtility.GetControlID(FocusType.Keyboard);
    }

    /**
     * Override this method to specify how tall the GUI for this field is in pixels.
     * The default is one line high.
     * 
     * @param SerializedProperty cProperty - The SerializedProperty to make the custom GUI for.
     * @param GUIContent cLabel - The label of this property.
     * @return float - The height in pixels.
     */
    public override float GetPropertyHeight(SerializedProperty cProperty, GUIContent cLabel)
    {
        bool bClassNameEmpty = string.IsNullOrEmpty(cProperty.FindPropertyRelative("m_strLuaClassName").stringValue);
        int nMethodsSize = cProperty.FindPropertyRelative("m_cMonoMethods").arraySize;
        int nGameObjSize = cProperty.FindPropertyRelative("m_aParameters").arraySize;
        nGameObjSize = (nGameObjSize > 0) ? nGameObjSize : 1;
        return m_fSingleLineHeight +    // Class name label.
            (bClassNameEmpty ? (m_fSingleLineGap + m_fSingleLineHeight * 2) : 0.0f) +                                   // The help box height.
            m_fSingleLineGap * 2 +      // Line gap.
            m_fSingleLineHeight +       // Method block header.
            m_fSingleLineGap + (m_fSingleLineHeight + m_fSingleLineGapEx) * (nMethodsSize + 1) + m_fSingleLineGap +     // Method block list.
            m_fSingleLineHeight +       // Method block footer.
            m_fSingleLineGap * 2 +      // Line gap.
            m_fSingleLineHeight +       // GameObject Parameters block header.
            m_fSingleLineGap + (m_fSingleLineHeight + m_fSingleLineGapEx) * nGameObjSize + m_fSingleLineGap +           // GameObject Parameters block list.
            m_fSingleLineHeight;        // GameObject Parameters block footer.
    }

    /**
     * Override this method to make your own GUI for the property.
     * 
     * @param Rect rcPosition - TheRectangle on the screen to use for the property GUI.
     * @param SerializedProperty cProperty - The SerializedProperty to make the custom GUI for.
     * @param GUIContent cLabel - The label of this property.
     * @return void.
     */
    public override void OnGUI(Rect rcPosition, SerializedProperty cProperty, GUIContent cLabel)
    {
        // Draw all the gui.
        Rect rcCurrent = OnClassNameGUI(rcPosition, cProperty, cLabel);
        rcCurrent = OnMethodsGUI(rcPosition, rcCurrent, cProperty, cLabel);
        OnParametersGUI(rcPosition, rcCurrent, cProperty, cLabel);
    }

    // Draw the class name gui.
    protected Rect OnClassNameGUI(Rect rcPosition, SerializedProperty cProperty, GUIContent cLabel)
    {
        // Calc the rect.
        Rect rcClassName = rcPosition;
        rcClassName.width -= 2.0f;
        rcClassName.height = m_fSingleLineHeight;

        // Get the whole area.
        Rect rcWholeArea = rcClassName;

        // Draw property field.
        EditorGUI.BeginProperty(rcClassName, cLabel, cProperty);
        EditorGUI.PropertyField(rcClassName, cProperty.FindPropertyRelative("m_strLuaClassName"), m_cLabelClassName);
        EditorGUI.EndProperty();

        // Check class name invalidation.
        if (string.IsNullOrEmpty(cProperty.FindPropertyRelative("m_strLuaClassName").stringValue))
        {
            // Show help box.
            Rect rcHelpBox = new Rect(rcClassName.x, rcClassName.yMax + m_fSingleLineGap, rcClassName.width, m_fSingleLineHeight * 2);
            EditorGUI.HelpBox(rcHelpBox, "You should specify a class name for Lua!", MessageType.Error);

            // Re-calc the whole area.
            rcWholeArea.yMax = rcHelpBox.yMax;
        }

        return rcWholeArea;
    }

    // Draw the mono behaviour method event gui.
    protected Rect OnMethodsGUI(Rect rcPosition, Rect rcLastRect, SerializedProperty cProperty, GUIContent cLabel)
    {
        // Get method list and count.
        SerializedProperty cMethodList = cProperty.FindPropertyRelative("m_cMonoMethods");
        int nMethodCount = cMethodList.arraySize;
        int nToBeRemovedMethod = -1;
        bool bRemoveAllMethod = false;
        float fElementSingleLineHeight = m_fSingleLineHeight + m_fSingleLineGapEx;

        // Get background header and box area.
        Rect rcHeader = new Rect(rcPosition.x, rcLastRect.yMax + m_fSingleLineGap * 2, rcPosition.width, m_fSingleLineHeight);
        Rect rcBoxBackground = new Rect(rcPosition.x, rcHeader.yMax, rcPosition.width, m_fSingleLineGap + fElementSingleLineHeight * (nMethodCount + 1) + m_fSingleLineGap);

        // Get footer background.
        float fFooterBgROffset = rcPosition.xMax - m_fFooterROffset - m_fFooterBtnROffset * 1;
        Rect rcFooterBackground = new Rect(fFooterBgROffset, rcBoxBackground.yMax, rcBoxBackground.xMax - fFooterBgROffset, m_fSingleLineHeight);
        Rect rcFooterBtnAdd = new Rect(fFooterBgROffset + m_fFooterROffsetEx, rcFooterBackground.y - m_fFooterROffsetEx, m_fFooterBtnWidth, m_fFooterBtnHeight);
        Rect rcFooterBtnMinus = new Rect(rcHeader.xMax - m_fFooterROffsetEx - m_fFooterBtnROffset, rcHeader.y, m_fFooterBtnWidth, m_fFooterBtnHeight);

        // Get the whole area.
        Rect rcWholeArea = new Rect(rcHeader.x, rcHeader.y, rcHeader.width, rcFooterBackground.yMax - rcHeader.y);

        // Draw background gui style.
        DrawGUIStyle(m_cGsHeaderBackground, rcHeader, false, false, false, false);
        DrawGUIStyle(m_cGsBoxBackground, rcBoxBackground, false, false, false, false);
        DrawGUIStyle(m_cGsFooterBackground, rcFooterBackground, false, false, false, false);

        // Begin to edit property.
        EditorGUI.BeginProperty(rcWholeArea, cLabel, cProperty);

        // Draw header string.
        Rect rcHeaderLabel = new Rect(rcHeader.x + m_fElementLROffset, rcHeader.y, rcHeader.width - m_fElementLROffset * 2, rcHeader.height);
        EditorGUI.LabelField(rcHeaderLabel, "Select MonoBehaviour event for Lua.");

        // Draw clear list button.
        EditorGUI.BeginDisabledGroup(nMethodCount <= 0);
        if (GUI.Button(rcFooterBtnMinus, m_cIconToolbarMinus, m_cGsPreButton))
        {
            bRemoveAllMethod = true;
        }

        EditorGUI.EndDisabledGroup();

        // Draw method list.
        Rect rcElementBg = new Rect(rcBoxBackground.x + m_fElementLROffsetEx2, rcBoxBackground.y + m_fSingleLineGap, rcBoxBackground.width - m_fElementLROffsetEx - m_fElementLROffsetEx2, fElementSingleLineHeight);
        Rect rcElementLabel = new Rect(rcBoxBackground.x + m_fElementLROffset, rcBoxBackground.y + m_fSingleLineGap + (m_fSingleLineGapEx / 2), rcBoxBackground.width - m_fElementLROffset, fElementSingleLineHeight);

        // Draw method bg list box.
        Color clrBgLb = GUI.backgroundColor;
        GUI.backgroundColor = Color.cyan;
        GUI.Box(rcElementBg, "");
        GUI.backgroundColor = clrBgLb;

        // Set the "Awake" field.
        EditorGUI.LabelField(rcElementLabel, "Awake (Built-in)");

        // Draw the method list.
        for (int i = 0; i < nMethodCount; i++)
        {
            // Re-calc the rect of the element.
            rcElementBg.y = rcElementBg.yMax;
            rcElementLabel.y = rcElementLabel.yMax;

            // Draw the element bg box.
            Color clrBg = GUI.backgroundColor;
            GUI.backgroundColor = Color.black;
            GUI.Box(rcElementBg, "");
            GUI.backgroundColor = clrBg;

            // Draw the element name and the remove button.
            EditorGUI.LabelField(rcElementLabel, Enum.GetName(typeof(YwLuaMonoBehaviourParams.EMonoMethod), cMethodList.GetArrayElementAtIndex(i).enumValueIndex));
            if (GUI.Button(new Rect(rcElementBg.xMax - m_fElementRemoveBtnLROffset, rcElementBg.y + m_fSingleLineGap, m_fElementRemoveBtnSize, m_fElementRemoveBtnSize), m_cIconToolbarMinus, m_cGsInvisibleRemoveButton))
            {
                nToBeRemovedMethod = i;
            }
        }

        // Draw the add new method menu.
        if (GUI.Button(rcFooterBtnAdd, m_cIconToolbarPlusMore, m_cGsPreButton))
        {
            ShowMethodEventMenu(cMethodList);
        }

        // Remove the element selected.
        if (nToBeRemovedMethod > -1)
        {
            cMethodList.DeleteArrayElementAtIndex(nToBeRemovedMethod);
        }

        // Remove all the elements.
        if (bRemoveAllMethod)
        {
            cMethodList.ClearArray();
        }

        // End edit.
        EditorGUI.EndProperty();

        return rcWholeArea;
    }

    // Draw the parameters gui.
    protected Rect OnParametersGUI(Rect rcPosition, Rect rcLastRect, SerializedProperty cProperty, GUIContent cLabel)
    {
        // Get game object list and count.
        SerializedProperty cGameObjList = cProperty.FindPropertyRelative("m_aParameters");
        bool bListIsEmpty = cGameObjList.arraySize <= 0;
        int nGameObjCount = bListIsEmpty ? 1 : cGameObjList.arraySize;
        int nToBeRemovedGameObj = -1;
        bool bRemoveAllGameObj = false;
        float fElementSingleLineHeight = m_fSingleLineHeight + m_fSingleLineGapEx;

        // Get background header and box area.
        Rect rcHeader = new Rect(rcPosition.x, rcLastRect.yMax + m_fSingleLineGap * 2, rcPosition.width, m_fSingleLineHeight);
        Rect rcBoxBackground = new Rect(rcPosition.x, rcHeader.yMax, rcPosition.width, m_fSingleLineGap + fElementSingleLineHeight * nGameObjCount + m_fSingleLineGap);

        // Get footer background.
        float fFooterBgROffset = rcPosition.xMax - m_fFooterROffset - m_fFooterBtnROffset * 1;
        Rect rcFooterBackground = new Rect(fFooterBgROffset, rcBoxBackground.yMax, rcBoxBackground.xMax - fFooterBgROffset, m_fSingleLineHeight);
        Rect rcFooterBtnAdd = new Rect(fFooterBgROffset + m_fFooterROffsetEx, rcFooterBackground.y - m_fFooterROffsetEx, m_fFooterBtnWidth, m_fFooterBtnHeight);
        Rect rcFooterBtnMinus = new Rect(rcHeader.xMax - m_fFooterROffsetEx - m_fFooterBtnROffset, rcHeader.y, m_fFooterBtnWidth, m_fFooterBtnHeight);
        Rect rcWholeArea = new Rect(rcHeader.x, rcHeader.y, rcHeader.width, rcFooterBackground.yMax - rcHeader.y);

        // Draw background gui style.
        DrawGUIStyle(m_cGsHeaderBackground, rcHeader, false, false, false, false);
        DrawGUIStyle(m_cGsBoxBackground, rcBoxBackground, false, false, false, false);
        DrawGUIStyle(m_cGsFooterBackground, rcFooterBackground, false, false, false, false);

        // Begin to edit property.
        EditorGUI.BeginProperty(rcWholeArea, cLabel, cProperty);

        // Draw header string.
        Rect rcHeaderLabel = new Rect(rcHeader.x + m_fElementLROffset, rcHeader.y, rcHeader.width - m_fElementLROffset * 2, rcHeader.height);
        EditorGUI.LabelField(rcHeaderLabel, "Select GameObjects as parameters for Lua.");

        // Draw clear list button.
        EditorGUI.BeginDisabledGroup(bListIsEmpty);
        if (GUI.Button(rcFooterBtnMinus, m_cIconToolbarMinus, m_cGsPreButton))
        {
            bRemoveAllGameObj = true;
        }

        EditorGUI.EndDisabledGroup();

        // Draw game object list.
        Rect rcElementBg = new Rect(rcBoxBackground.x + m_fElementLROffsetEx2, rcBoxBackground.y + m_fSingleLineGap, rcBoxBackground.width - m_fElementLROffsetEx - m_fElementLROffsetEx2, fElementSingleLineHeight);
        Rect rcElementLabel = new Rect(rcBoxBackground.x + m_fElementLROffset, rcBoxBackground.y + m_fSingleLineGap + (m_fSingleLineGapEx / 2), rcBoxBackground.width - m_fElementLROffset, fElementSingleLineHeight);

        // Leave at least one element space.
        if (bListIsEmpty)
        {
            EditorGUI.LabelField(rcElementLabel, "List is empty!");
        }
        else
        {
            // Draw all the field list.
            for (int i = 0; i < nGameObjCount; i++)
            {
                // Draw the element bg box.
                Color clrBg = GUI.backgroundColor;
                GUI.backgroundColor = Color.black;
                GUI.Box(rcElementBg, "");
                GUI.backgroundColor = clrBg;

                // Re-calc the rect of the element.
                Rect rcElementObjField = rcElementLabel;
                rcElementObjField.width -= m_fFooterBtnWidth * 2;
                rcElementObjField.height = m_fSingleLineHeight;

                EditorGUI.BeginChangeCheck();
                UnityEngine.Object cSelObj = EditorGUI.ObjectField(rcElementObjField, new GUIContent("GameObject " + i), cGameObjList.GetArrayElementAtIndex(i).objectReferenceValue, typeof(GameObject), true);
                if (EditorGUI.EndChangeCheck())
                {
                    cGameObjList.GetArrayElementAtIndex(i).objectReferenceValue = cSelObj;
                }

                // Draw the remove button.
                if (GUI.Button(new Rect(rcElementBg.xMax - m_fElementRemoveBtnLROffset, rcElementBg.y + m_fSingleLineGap, m_fElementRemoveBtnSize, m_fElementRemoveBtnSize), m_cIconToolbarMinus, m_cGsInvisibleRemoveButton))
                {
                    nToBeRemovedGameObj = i;
                }

                // Re-calc the rect of the element bg.
                rcElementBg.y = rcElementBg.yMax;
                rcElementLabel.y = rcElementLabel.yMax;
            }
        }

        // Draw the add new game object menu.
        if (GUI.Button(rcFooterBtnAdd, m_cIconToolbarPlus, m_cGsPreButton))
        {
            int nInsertPos = bListIsEmpty ? 0 : nGameObjCount;
            cGameObjList.InsertArrayElementAtIndex(nInsertPos);
            cGameObjList.GetArrayElementAtIndex(nInsertPos).objectReferenceValue = bListIsEmpty ? null : cGameObjList.GetArrayElementAtIndex(nInsertPos - 1).objectReferenceValue;
        }

        // Remove the element selected.
        if (nToBeRemovedGameObj > -1)
        {
            cGameObjList.GetArrayElementAtIndex(nToBeRemovedGameObj).objectReferenceValue = null;
            cGameObjList.DeleteArrayElementAtIndex(nToBeRemovedGameObj);
        }

        // Remove all the elements.
        if (bRemoveAllGameObj)
        {
            cGameObjList.ClearArray();
        }

        // End edit.
        EditorGUI.EndProperty();

        return rcWholeArea;
    }

    // Show the available mono behaviour method list menu.
    protected void ShowMethodEventMenu(SerializedProperty cMethodList)
    {
        // Now create the menu, add items and show it.
        GenericMenu cMenu = new GenericMenu();
        Array aMethodEnum = Enum.GetValues(typeof(YwLuaMonoBehaviourParams.EMonoMethod));
        for (int i = 0; i < aMethodEnum.Length; i++)
        {
            bool bActive = true;
            int nEventIndex = (int)aMethodEnum.GetValue(i);

            // Check if we already have a Entry for the current eventType, if so, disable it.
            for (int j = 0; j < cMethodList.arraySize; j++)
            {
                SerializedProperty cEvent = cMethodList.GetArrayElementAtIndex(j);
                if (cEvent.enumValueIndex == nEventIndex)
                {
                    bActive = false;
                    break;
                }
            }

            if (bActive)
            {
                cMenu.AddItem(new GUIContent(aMethodEnum.GetValue(i).ToString()), false, MethodEventMenuCallback, new MethodMenuEventParam(cMethodList, nEventIndex));
            }
            else
            {
                cMenu.AddDisabledItem(new GUIContent(aMethodEnum.GetValue(i).ToString()));
            }
        }

        cMenu.ShowAsContext();
        Event.current.Use();
    }

    // The mono behaviour method list menu click callback.
    protected void MethodEventMenuCallback(object cEventParam)
    {
        // Get the listener and the event enum.
        MethodMenuEventParam cEventInfo = (MethodMenuEventParam)cEventParam;
        int nEventSize = cEventInfo.m_cListener.arraySize;
        int nInsertPos = nEventSize;
        for (int i = nEventSize - 1; i >= 0; i--)
        {
            if (cEventInfo.m_nEventIdx < cEventInfo.m_cListener.GetArrayElementAtIndex(i).enumValueIndex)
            {
                nInsertPos = i;
            }
        }

        // Add a new method to the list.
        cEventInfo.m_cListener.InsertArrayElementAtIndex(nInsertPos);
        cEventInfo.m_cListener.GetArrayElementAtIndex(nInsertPos).enumValueIndex = cEventInfo.m_nEventIdx;
        cEventInfo.m_cListener.serializedObject.ApplyModifiedProperties();
    }

    // Draw gui style.
    // bIsActive and bIsOn both true give the selected style, plus bHasKeyboardFocus true give the bule bg that shows the focused selected style.
    protected void DrawGUIStyle(GUIStyle cStyle, Rect rcSize, bool bIsHover, bool bIsActive, bool bIsOn, bool bHasKeyboardFocus)
    {
        if (EventType.Repaint != Event.current.type)
        {
            return;
        }

        cStyle.Draw(rcSize, bIsHover, bIsActive, bIsOn, bHasKeyboardFocus);
    }

    // If has the keyboard control.
    protected bool HasKeyboardControl()
    {
        return GUIUtility.keyboardControl == m_nControlID;
    }
}

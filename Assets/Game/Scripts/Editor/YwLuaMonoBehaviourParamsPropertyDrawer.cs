using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

[CustomPropertyDrawer(typeof(YwLuaMonoBehaviourParams))]
public class YwLuaMonoBehaviourParamsPropertyDrawer : PropertyDrawer
{
    protected static readonly GUIContent m_cIconToolbarPlus = EditorGUIUtility.IconContent("Toolbar Plus", "Add to list.");
    protected static readonly GUIContent m_cIconToolbarPlusMore = EditorGUIUtility.IconContent("Toolbar Plus More", "Choose to add to list.");
    protected static readonly GUIContent m_cIconToolbarMinus = EditorGUIUtility.IconContent("Toolbar Minus", "Remove selection from list.");

    protected static readonly GUIContent m_cLabelClassName = new GUIContent("Lua Class Name");

    protected static readonly GUIStyle m_cGsHeaderBackground = "RL Header";
    protected static readonly GUIStyle m_cGsBoxBackground = "RL Background";
    protected static readonly GUIStyle m_cGsFooterBackground = "RL Footer";
    protected static readonly GUIStyle m_cGsPreButton = "RL FooterButton";
    protected static readonly GUIStyle m_cGsElementBackground = "RL Element";
    protected static readonly GUIStyle m_cGsElementBackground2 = "OL Box";
    protected static readonly GUIStyle m_cGsInvisibleRemoveButton = "InvisibleButton";

    protected static readonly float m_fSingleLineHeight = EditorGUIUtility.singleLineHeight;
    protected static readonly float m_fSingleLineGap = 3.0f;
    protected static readonly float m_fFooterROffset = 8.0f;
    protected static readonly float m_fFooterROffsetEx = 4.0f;
    protected static readonly float m_fFooterBtnROffset = 25.0f;
    protected static readonly float m_fFooterBtnWidth = 25.0f;
    protected static readonly float m_fFooterBtnHeight = 13.0f;
    protected static readonly float m_fElementLROffset = 6.0f;
    protected static readonly float m_fElementLROffsetEx = 2.0f;
    protected static readonly float m_fElementRemoveBtnSize = 16.0f;
    protected static readonly float m_fElementRemoveBtnLROffset = 22.0f;

    private int m_nControlID = -1;

    public override float GetPropertyHeight(SerializedProperty cProperty, GUIContent cLabel)
    {
        return m_fSingleLineHeight +    // Class name label.
            m_fSingleLineGap * 2 +      // Line gap.
            m_fSingleLineHeight +       // Method block header.
            m_fSingleLineGap + (m_fSingleLineHeight + m_fSingleLineGap) * (cProperty.FindPropertyRelative("m_cMonoMethods").arraySize + 1) + m_fSingleLineGap +    // Method block list.
            m_fSingleLineHeight;        // Method block footer.
    }

    public override void OnGUI(Rect rcPosition, SerializedProperty cProperty, GUIContent cLabel)
    {
        Rect rcCurrent = OnClassNameGUI(rcPosition, cProperty, cLabel);
        rcCurrent = OnMethodsGUI(rcPosition, rcCurrent, cProperty, cLabel);
        OnParametersGUI(rcPosition, rcCurrent, cProperty, cLabel);
    }

    protected Rect OnClassNameGUI(Rect rcPosition, SerializedProperty cProperty, GUIContent cLabel)
    {
        Rect rcClassName = rcPosition;
        rcClassName.width -= 2.0f;
        rcClassName.height = m_fSingleLineHeight;

        EditorGUI.BeginProperty(rcClassName, cLabel, cProperty);
        EditorGUI.PropertyField(rcClassName, cProperty.FindPropertyRelative("m_strLuaClassName"), m_cLabelClassName);
        EditorGUI.EndProperty();

        return rcClassName;
    }

    protected Rect OnMethodsGUI(Rect rcPosition, Rect rcLastRect, SerializedProperty cProperty, GUIContent cLabel)
    {
        SerializedProperty cMethodList = cProperty.FindPropertyRelative("m_cMonoMethods");
        int nMethodCount = cMethodList.arraySize;
        float fElementSingleLineHeight = m_fSingleLineHeight + m_fSingleLineGap;

        Rect rcHeader = new Rect(rcPosition.x, rcLastRect.yMax + m_fSingleLineGap * 2, rcPosition.width, m_fSingleLineHeight);
        Rect rcBoxBackground = new Rect(rcPosition.x, rcHeader.yMax, rcPosition.width, m_fSingleLineGap + fElementSingleLineHeight * (nMethodCount + 1) + m_fSingleLineGap);

        float fFooterBgROffset = rcPosition.xMax - m_fFooterROffset - m_fFooterBtnROffset * 1;
        Rect rcFooterBackground = new Rect(fFooterBgROffset, rcBoxBackground.yMax, rcBoxBackground.xMax - fFooterBgROffset, m_fSingleLineHeight);

        Rect rcFooterBtnAdd = new Rect(fFooterBgROffset + m_fFooterROffsetEx, rcFooterBackground.y - m_fFooterROffsetEx, m_fFooterBtnWidth, m_fFooterBtnHeight);
        //Rect rcFooterBtnMinus = new Rect(rcFooterBackground.xMax - m_fFooterROffsetEx - m_fFooterBtnROffset, rcFooterBackground.y - 3.0f, m_fFooterBtnWidth, m_fFooterBtnHeight);

        // Draw method gui style.
        DrawGUIStyle(m_cGsHeaderBackground, rcHeader, false, false, false, false);
        DrawGUIStyle(m_cGsBoxBackground, rcBoxBackground, false, false, false, false);
        DrawGUIStyle(m_cGsFooterBackground, rcFooterBackground, false, false, false, false);

        // Draw header string.
        Rect rcHeaderLabel = new Rect(rcHeader.x + m_fElementLROffset, rcHeader.y, rcHeader.width - m_fElementLROffset * 2, rcHeader.height);
        EditorGUI.LabelField(rcHeaderLabel, "Select MonoBehaviour event for Lua.");

        // Draw method list.
        Rect rcElementBg = new Rect(rcBoxBackground.x, rcBoxBackground.y + m_fSingleLineGap, rcBoxBackground.width, fElementSingleLineHeight);
        Rect rcElementLabel = new Rect(rcBoxBackground.x + m_fElementLROffset, rcBoxBackground.y + m_fSingleLineGap, rcBoxBackground.width, fElementSingleLineHeight);
        DrawGUIStyle(m_cGsElementBackground2, new Rect(rcElementBg.x, rcElementBg.y, rcElementBg.width - m_fElementLROffsetEx, rcElementBg.height), false, false, false, false);
        EditorGUI.LabelField(rcElementLabel, "Awake (Built-in)");

        Color clrBg = GUI.backgroundColor;
        GUI.backgroundColor = Color.black;

        rcElementBg.width -= m_fElementLROffsetEx;
        for (int i = 0; i < nMethodCount; i++)
        {
            rcElementBg.y = rcElementBg.yMax;
            rcElementLabel.y = rcElementLabel.yMax;
            //DrawGUIStyle(m_cGsElementBackground, rcElementBg, false, true, true, true);
            GUI.Box(rcElementBg, "");
            EditorGUI.LabelField(rcElementLabel, Enum.GetName(typeof(YwLuaMonoBehaviourParams.EMonoMethod), cMethodList.GetArrayElementAtIndex(i).enumValueIndex));
            GUI.Button(new Rect(rcElementBg.xMax - m_fElementRemoveBtnLROffset, rcElementBg.y + m_fSingleLineGap, m_fElementRemoveBtnSize, m_fElementRemoveBtnSize), m_cIconToolbarMinus, m_cGsInvisibleRemoveButton);
        }

        GUI.backgroundColor = clrBg;

        GUI.Button(rcFooterBtnAdd, m_cIconToolbarPlus, m_cGsPreButton);
        //GUI.Button(rcFooterBtnMinus, m_cIconToolbarMinus, m_cGsPreButton);

        return rcHeader;
    }

    protected void OnParametersGUI(Rect rcPosition, Rect rcLastRect, SerializedProperty cProperty, GUIContent cLabel)
    {

    }

    protected void DrawGUIStyle(GUIStyle cStyle, Rect rcSize, bool bIsHover, bool bIsActive, bool bIsOn, bool bHasKeyboardFocus)
    {
        if (EventType.Repaint != Event.current.type)
        {
            return;
        }

        cStyle.Draw(rcSize, bIsHover, bIsActive, bIsOn, bHasKeyboardFocus);
    }

    protected bool HasKeyboardControl()
    {
        if (-1 == m_nControlID)
        {
            m_nControlID = GUIUtility.GetControlID(FocusType.Keyboard);
        }

        return GUIUtility.keyboardControl == m_nControlID;
    }
}

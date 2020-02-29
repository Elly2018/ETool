using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ETool
{
    public enum StyleType
    {
        Default_Node,
        Event_Node,
        Logic_Node,
        Function_Node,
        Error_Node,

        Select_Default_Node,
        Select_Event_Node,
        Select_Logic_Node,
        Select_Function_Node,
        Select_Error_Node,

        In_Point,
        Out_Point,
        In_Point_Array,
        Out_Point_Array,

        Panel,
        GUI_Title,
        GUI_Properties,

        BoxSelect,
        BoxDeleteSelect
    }

    /* Define how sisters getting draw hehe */
    public class StyleUtility
    {
#if UNITY_EDITOR
        private static GUIStyle[] m_GUIStyle = new GUIStyle[0];

        public static void Initialize()
        {
            m_GUIStyle = new GUIStyle[Enum.GetNames(typeof(StyleType)).Length];
            for(int i = 0; i < m_GUIStyle.Length; i++)
            {
                m_GUIStyle[i] = CreateStyle((StyleType)i);
            }
        }

        private static GUIStyle CreateStyle(StyleType select)
        {
            string themeColorString = string.Empty;


            GUIStyle result = new GUIStyle();
            switch (select)
            {
                #region Default
                case StyleType.Default_Node: // 0
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);

                    return result;
                case StyleType.Event_Node: // 1
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node2@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Logic_Node: // 2
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node3@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Function_Node: // 3
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node4@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Error_Node: // 4
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node6@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;
                #endregion

                #region Select
                case StyleType.Select_Default_Node: // 30
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Event_Node: // 31
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node2 on@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Logic_Node: // 32
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node3 on@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Function_Node: // 33
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node4 on@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Error_Node:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node6 on@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;
                #endregion

                case StyleType.In_Point: // 60
                    result.normal.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets\\ETool\\Texture2D\\ObjectNode.png") as Texture2D;
                    result.active.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets\\ETool\\Texture2D\\ObjectNode.png") as Texture2D;
                    return result;

                case StyleType.Out_Point: // 61
                    result.normal.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets\\ETool\\Texture2D\\ObjectNode.png") as Texture2D;
                    result.active.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets\\ETool\\Texture2D\\ObjectNode.png") as Texture2D;
                    return result;

                case StyleType.In_Point_Array: // 62
                    result.normal.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets\\ETool\\Texture2D\\ArrayNode.png") as Texture2D;
                    result.active.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets\\ETool\\Texture2D\\ArrayNode.png") as Texture2D;
                    return result;

                case StyleType.Out_Point_Array: // 63
                    result.normal.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets\\ETool\\Texture2D\\ArrayNode.png") as Texture2D;
                    result.active.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets\\ETool\\Texture2D\\ArrayNode.png") as Texture2D;
                    return result;

                case StyleType.Panel: // 90
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/objectpickerbackground.png") as Texture2D;
                    return result;

                case StyleType.GUI_Title: // 120
                    result.alignment = TextAnchor.MiddleCenter;
                    result.fontStyle = FontStyle.Bold;
                    result.wordWrap = true;
                    return result;

                case StyleType.GUI_Properties: // 121
                    result.alignment = TextAnchor.MiddleCenter;
                    result.normal.background = GetTexBG();
                    return result;

                case StyleType.BoxSelect:
                    result.normal.background = new Texture2D(1, 1);
                    result.normal.background.SetPixel(0, 0, new Color(1, 1, 1, 0.5f));
                    result.normal.background.Apply();
                    return result;

                case StyleType.BoxDeleteSelect:
                    result.normal.background = new Texture2D(1, 1);
                    result.normal.background.SetPixel(0, 0, new Color(1, 0, 0, 0.5f));
                    result.normal.background.Apply();
                    return result;
            }
            return result;
        }

        private static Texture2D GetTexBG()
        {
            Texture2D t = new Texture2D(5, 5);
            for (int y = 0; y < t.height; y++)
                for (int x = 0; x < t.width; x++)
                    t.SetPixel(x, y, new Color(0.8f, 0.8f, 0.8f, 0.5f));
            t.Apply();
            return t;
        }
#endif

        public static GUIStyle GetStyle(StyleType select)
        {
#if UNITY_EDITOR
            if (m_GUIStyle.Length == 0 || m_GUIStyle == null)
            {
                Initialize();
            }
            if (m_GUIStyle[(int)select] == null) 
            {
                m_GUIStyle[(int)select] = CreateStyle(select);
            }
            return m_GUIStyle[(int)select];
#else
            return new GUIStyle();
#endif
        }

    }
}
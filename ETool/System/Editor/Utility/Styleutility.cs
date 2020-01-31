using UnityEditor;
using UnityEngine;

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

        Panel = 90,
        GUI_Title = 120,
        GUI_Properties = 121
    }

    /* Define how sisters getting draw hehe */
    public class StyleUtility
    {
        public static GUIStyle GetStyle(StyleType select)
        {
            string themeColorString = string.Empty;

            if (NodeBasedEditor.uITheme == GUITheme.Light)
                themeColorString = "lightskin";
            if (NodeBasedEditor.uITheme == GUITheme.Dark)
                themeColorString = "darkskin";

            GUIStyle result = new GUIStyle();
            switch (select)
            {
                #region Default
                case StyleType.Default_Node: // 0
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node1@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);

                    return result;
                case StyleType.Event_Node: // 1
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node2@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Logic_Node: // 2
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node3@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Function_Node: // 3
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node4@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Error_Node: // 4
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node6@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;
                #endregion

                #region Select
                case StyleType.Select_Default_Node: // 30
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node1 on@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Event_Node: // 31
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node2 on@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Logic_Node: // 32
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node3 on@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Function_Node: // 33
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node4 on@2x.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Error_Node:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node6 on@2x.png") as Texture2D;
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
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/objectpickerbackground.png") as Texture2D;
                    return result;

                case StyleType.GUI_Title: // 120
                    result.alignment = TextAnchor.MiddleCenter;
                    result.fontStyle = FontStyle.Bold;
                    return result;

                case StyleType.GUI_Properties: // 121
                    result.alignment = TextAnchor.MiddleCenter;
                    result.normal.background = new Texture2D(10, 10);
                    return result;
            }
            return result;
        }

    }
}

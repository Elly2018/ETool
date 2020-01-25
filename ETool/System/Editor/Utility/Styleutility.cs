using UnityEditor;
using UnityEngine;

namespace ETool
{
    public enum StyleType
    {
        Default_Node = 0,
        Event_Node = 1,
        Logic_Node = 2,
        Function_Node = 3,
        Select_Default_Node = 30,
        Select_Event_Node = 31,
        Select_Logic_Node = 32,
        Select_Function_Node = 33,
        In_Point = 60,
        Out_Point = 61,
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
                case StyleType.Default_Node:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node1.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);

                    return result;
                case StyleType.Event_Node:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node2.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Logic_Node:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node3.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Function_Node:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node4.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Default_Node:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node1 on.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Event_Node:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node2 on.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Logic_Node:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node3 on.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.Select_Function_Node:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/node4 on.png") as Texture2D;
                    result.border = new RectOffset(12, 12, 12, 12);
                    return result;

                case StyleType.In_Point:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
                    result.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
                    result.border = new RectOffset(4, 4, 12, 12);
                    return result;

                case StyleType.Out_Point:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
                    result.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
                    result.border = new RectOffset(4, 4, 12, 12);
                    return result;

                case StyleType.Panel:
                    result.normal.background = EditorGUIUtility.Load("builtin skins/" + themeColorString + "/images/objectpickerbackground.png") as Texture2D;
                    return result;

                case StyleType.GUI_Title:
                    result.alignment = TextAnchor.MiddleCenter;
                    result.fontStyle = FontStyle.Bold;
                    return result;

                case StyleType.GUI_Properties:
                    result.alignment = TextAnchor.MiddleCenter;
                    result.normal.background = new Texture2D(10, 10);
                    return result;

                default:
                    return result;
            }
        }

    }
}

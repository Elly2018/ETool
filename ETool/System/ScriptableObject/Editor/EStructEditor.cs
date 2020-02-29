#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    [CustomEditor(typeof(EStruct))]
    [CanEditMultipleObjects]
    public class EStructEditor : EToolEditorBase
    {
        private Rect buttonRect;

        public override void DrawEToolInformation()
        {
            EStruct myTarget = (EStruct)target;
            BlueprintVariable[] se = myTarget.structBase.structElement.ToArray();

            VGroupStart();
            DrawTopLabel();
            for (int i = 0; i < se.Length; i++)
            {
                HGroupStart();
                DrawElementLabel(se[i]);

                if (GUILayout.Button(se[i].type.ToString(), GUILayout.Width(200)))
                {
                    PopupWindow.Show(buttonRect, new ETypeSelection(se[i]));
                }
                if (Event.current.type == EventType.Repaint) buttonRect = GUILayoutUtility.GetLastRect();

                DrawElementContainerType(se[i]);
                DrawElementDefaultValue(se[i]);
                if (GUILayout.Button("-"))
                    myTarget.structBase.structElement.RemoveAt(i);
                HGroupEnd();
            }
            if (GUILayout.Button("+"))
                myTarget.structBase.structElement.Add(new BlueprintVariable());
            VGroupEnd();
        }

        ///
        ///  The region where function use for gui drawing
        ///
        #region Draw
        /// <summary>
        /// Center the top label string
        /// </summary>
        /// <returns></returns>
        private GUIStyle centerSkin()
        {
            GUIStyle defaultSkin = new GUIStyle();
            defaultSkin.alignment = TextAnchor.MiddleCenter;
            return defaultSkin;
        }

        /// <summary>
        /// Drawing the top label
        /// </summary>
        private void DrawTopLabel()
        {
            HGroupStart();
            EditorGUILayout.LabelField("Label", centerSkin(), GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Data Type", centerSkin(), GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Struct Type", centerSkin(), GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Default / Array Size", centerSkin(), GUILayout.MinWidth(100));
            HGroupEnd();
        }

        /// <summary>
        /// Element's label
        /// </summary>
        /// <param name="e"></param>
        private void DrawElementLabel(BlueprintVariable e)
        {
            e.label = EditorGUILayout.TextField(e.label, GUILayout.MinWidth(100));
        }

        /// <summary>
        /// Element's container type
        /// </summary>
        /// <param name="e"></param>
        private void DrawElementContainerType(BlueprintVariable e)
        {
            e.fieldContainer = (FieldContainer)EditorGUILayout.EnumPopup(e.fieldContainer);
        }

        /// <summary>
        /// Element's default value
        /// </summary>
        /// <param name="e"></param>
        private void DrawElementDefaultValue(BlueprintVariable e)
        {
            if(e.fieldContainer == FieldContainer.Array)
            {
                e.variable.genericBasicType.target_Int = EditorGUILayout.IntField(e.variable.genericBasicType.target_Int);
            }
            else
            {
                e.variable = Field.DrawFieldHelper(e.variable, e.type);
            }
        }
        #endregion
    }
}
#endif
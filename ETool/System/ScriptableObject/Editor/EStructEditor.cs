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
        public override void DrawEToolInformation()
        {
            EStruct myTarget = (EStruct)target;
            StructElement[] se = myTarget.structBase.structElement.ToArray();

            VGroupStart();
            DrawTopLabel();
            for (int i = 0; i < se.Length; i++)
            {
                HGroupStart();
                DrawElementLabel(se[i]);
                se[i].type = (FieldType)EditorGUILayout.EnumPopup(se[i].type);
                DrawElementContainerType(se[i]);
                DrawElementDefaultValue(se[i]);
                if (GUILayout.Button("-"))
                    myTarget.structBase.structElement.RemoveAt(i);
                HGroupEnd();
            }
            if (GUILayout.Button("+"))
                myTarget.structBase.structElement.Add(new StructElement());
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
            EditorGUILayout.LabelField("Element Default", centerSkin(), GUILayout.MinWidth(100));
            HGroupEnd();
        }

        /// <summary>
        /// Element's label
        /// </summary>
        /// <param name="e"></param>
        private void DrawElementLabel(StructElement e)
        {
            e.label = EditorGUILayout.TextField(e.label, GUILayout.MinWidth(100));
        }

        /// <summary>
        /// Element's container type
        /// </summary>
        /// <param name="e"></param>
        private void DrawElementContainerType(StructElement e)
        {
            e.structDataType = EditorGUILayout.Popup(e.structDataType, Enum.GetNames(typeof(StructDataType)));
        }

        /// <summary>
        /// Element's default value
        /// </summary>
        /// <param name="e"></param>
        private void DrawElementDefaultValue(StructElement e)
        {
            if (e.structDataType != (int)StructDataType.Object)
                GUI.enabled = false;

            e.elementDefault = Field.DrawFieldHelper(e.elementDefault, e.type);

            GUI.enabled = true;
        }
        #endregion
    }
}
#endif
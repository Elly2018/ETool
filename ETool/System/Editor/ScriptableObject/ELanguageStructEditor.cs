using UnityEditor;
using UnityEngine;

namespace ETool
{
    [CustomEditor(typeof(ELanguageStruct))]
    [CanEditMultipleObjects]
    public class ELanguageStructEditor : EToolEditorBase
    {
        public override void DrawEToolInformation()
        {
            HGroupStart();
            if (GUILayout.Button("Reset Node"))
            {
                ((ELanguageStruct)target).Reset_Node_Func();
            }
            if (GUILayout.Button("Reset Field"))
            {
                ((ELanguageStruct)target).Reset_Field_Func();
            }
            if (GUILayout.Button("Reset Custom"))
            {
                ((ELanguageStruct)target).Reset_Custom_Field();
            }
            HGroupEnd();

            EditorGUILayout.Space();

            VGroupStart();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("languageElements"), true);
            VGroupEnd();
        }
    }
}


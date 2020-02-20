#if UNITY_EDITOR
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
            HGroupStart();
            if (GUILayout.Button("Reset Node Keep"))
            {
                ((ELanguageStruct)target).Reset_Node_Func_Keep();
            }
            if (GUILayout.Button("Reset Field Keep"))
            {
                ((ELanguageStruct)target).Reset_Field_Func_Keep();
            }
            HGroupEnd();
            HGroupStart();
            if (GUILayout.Button("Import Json"))
            {
                ((ELanguageStruct)target).Import();
            }
            if (GUILayout.Button("Export Json"))
            {
                ((ELanguageStruct)target).Export();
            }
            HGroupEnd();
            EditorGUILayout.Space();

            VGroupStart();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("languagePack"), true);
            VGroupEnd();
        }
    }
}
#endif
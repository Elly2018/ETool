using UnityEditor;
using UnityEngine;

namespace ETool
{
    [CustomEditor(typeof(EGameData))]
    [CanEditMultipleObjects]
    public class EGameDataEditor : EToolEditorBase
    {
        public override void DrawEToolInformation()
        {
            EditorGUI.BeginChangeCheck();
            EGameData g = (EGameData)target;

            /* Category */
            VGroupStart();
            for(int i = 0; i < g.gameDataStruct.gameDataCategories.Count; i++)
            {
                EditorGUILayout.Space();
                DrawTitle2(g.gameDataStruct.gameDataCategories[i].label);
                EditorGUILayout.Space();

                g.gameDataStruct.gameDataCategories[i].fold =
                    EditorGUILayout.Foldout(g.gameDataStruct.gameDataCategories[i].fold,
                    "");

                /* Element List */
                if (g.gameDataStruct.gameDataCategories[i].fold)
                {
                    GameDataCategory bufferCate =
                        g.gameDataStruct.gameDataCategories[i];
                    bufferCate.label = EditorGUILayout.TextField("Label", bufferCate.label);

                    EditorGUILayout.Space();

                    /* Element */
                    for(int j = 0; j < bufferCate.gameDataElements.Count; j++)
                    {
                        GameDataElement bufferElement =
                            bufferCate.gameDataElements[j];

                        HGroupStart();
                        bufferElement.label = EditorGUILayout.TextField(bufferElement.label);
                        bufferElement.fieldType = (FieldType)EditorGUILayout.EnumPopup(bufferElement.fieldType);
                        bufferElement.value = Field.DrawFieldHelper(bufferElement.value, bufferElement.fieldType);
                        HGroupEnd();
                    }

                    HGroupStart();
                    if (GUILayout.Button("Add Element"))
                    {
                        bufferCate.gameDataElements.Add(new GameDataElement());
                    }
                    if (GUILayout.Button("Clear Element"))
                    {
                        bufferCate.gameDataElements.Clear();
                    }
                    HGroupEnd();
                }

                EditorGUILayout.Space(); EditorGUILayout.Space();
            }
            HGroupStart();
            if (GUILayout.Button("Add Category"))
            {
                g.gameDataStruct.gameDataCategories.Add(new GameDataCategory());
            }
            if (GUILayout.Button("Clear Category"))
            {
                g.gameDataStruct.gameDataCategories.Clear();
            }
            HGroupEnd();
            VGroupEnd();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(g);
            }
        }
    }
}

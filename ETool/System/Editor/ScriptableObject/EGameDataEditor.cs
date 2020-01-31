using System;
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
                        BlueprintVariable bufferElement = 
                            bufferCate.gameDataElements[j];
                        HGroupStart();
                        bufferElement.fieldContainer = (FieldContainer)EditorGUILayout.EnumPopup(bufferElement.fieldContainer);
                        HGroupEnd();

                        VGroupStart();
                        if(bufferElement.fieldContainer == FieldContainer.Object)
                        {
                            HGroupStart();
                            bufferElement.label = EditorGUILayout.TextField(bufferElement.label);
                            bufferElement.type = (FieldType)EditorGUILayout.EnumPopup(bufferElement.type);
                            bufferElement.variable = Field.DrawFieldHelper(bufferElement.variable, bufferElement.type);
                            HGroupEnd();
                        }
                        if (bufferElement.fieldContainer == FieldContainer.Array)
                        {
                            bufferElement.fold = EditorGUILayout.Foldout(bufferElement.fold, bufferElement.label);
                            if (bufferElement.fold)
                            {
                                HGroupStart();
                                bufferElement.label = EditorGUILayout.TextField(bufferElement.label);
                                bufferElement.type = (FieldType)EditorGUILayout.EnumPopup(bufferElement.type);
                                HGroupEnd();
                                HGroupStart();
                                bufferElement.variable.genericBasicType.target_Int = EditorGUILayout.IntField("Size", bufferElement.variable.genericBasicType.target_Int);
                                if (bufferElement.variable.genericBasicType.target_Int < 0) bufferElement.variable.genericBasicType.target_Int = 0;
                                HGroupEnd();
                                Array.Resize(ref bufferElement.variable_Array, bufferElement.variable.genericBasicType.target_Int);
                                for (int k = 0; k < bufferElement.variable_Array.Length; k++)
                                {
                                    HGroupStart();
                                    try
                                    {
                                        bufferElement.variable_Array[k] = Field.DrawFieldHelper(bufferElement.variable_Array[k], bufferElement.type);
                                    }
                                    catch { }
                                    HGroupEnd();
                                }
                            }
                        }
                        VGroupEnd();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                    }

                    HGroupStart();
                    if (GUILayout.Button("Add Element"))
                    {
                        bufferCate.gameDataElements.Add(new BlueprintVariable());
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

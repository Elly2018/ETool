#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    public class ETypeSelection : PopupWindowContent
    {
        private int typeIndex = 0;
        private FieldType selection;
        private Vector2 scrollView;

        BlueprintVariable target;
        Action<string> update;
        Action<int> iupdate;
        int eventIndex;

        public ETypeSelection(BlueprintVariable e)
        {
            target = e;
        }

        public ETypeSelection(BlueprintVariable e, Action<string> u)
        {
            target = e;
            update = u;
        }

        public ETypeSelection(BlueprintVariable e, Action<int> u, int eventIndex)
        {
            target = e;
            iupdate = u;
            this.eventIndex = eventIndex;
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(400, 300);
        }

        public override void OnClose()
        {
            base.OnClose();
        }

        public override void OnGUI(Rect rect)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Basic Type", GUILayout.Height(25)))
            {
                typeIndex = 1;
            }
            if (GUILayout.Button("Unity Type", GUILayout.Height(25)))
            {
                typeIndex = 2;
            }
            if (GUILayout.Button("Component Type", GUILayout.Height(25)))
            {
                typeIndex = 3;
            }
            if (GUILayout.Button("Enum Type", GUILayout.Height(25)))
            {
                typeIndex = 4;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            EnumUseStruct[] enumUseStructs = null;
            switch (typeIndex)
            {
                case 1:
                    enumUseStructs = Field.GetFieldTypeEnumUseStruct(10, 49);
                    break;
                case 2:
                    enumUseStructs = Field.GetFieldTypeEnumUseStruct(50, 199);
                    break;
                case 3:
                    enumUseStructs = Field.GetFieldTypeEnumUseStruct(200, 1999);
                    break;
                case 4:
                    enumUseStructs = Field.GetFieldTypeEnumUseStruct(2000, 99999);
                    break;
            }

            if (enumUseStructs != null)
            {
                scrollView = EditorGUILayout.BeginScrollView(scrollView);
                if (typeIndex != 0)
                {
                    for (int i = 0; i < enumUseStructs.Length; i++)
                    {
                        if (GUILayout.Button(enumUseStructs[i].fieldName))
                        {
                            selection = (FieldType)enumUseStructs[i].fieldIndex;
                            target.type = selection;

                            if (update != null)
                                update.Invoke(target.label);
                            if (iupdate != null)
                                iupdate.Invoke(eventIndex);
                        }
                    }
                }
                EditorGUILayout.EndScrollView();
            }
        }

        public override void OnOpen()
        {
            base.OnOpen();
        }
    }
}
#endif
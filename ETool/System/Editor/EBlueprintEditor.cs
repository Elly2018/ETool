using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    [CustomEditor(typeof(EBlueprint))]
    [CanEditMultipleObjects]
    public class EBlueprintEditor : EToolEditorBase
    {
        public override string GetInfoString()
        {
            return this.name;
        }

        public override void DrawEToolInformation()
        {
            EBlueprint b = (EBlueprint)target;
            bool anyVariableLabelEmpty = false;
            bool repeatVLabelName = false;
            bool eventAnyArugmentLabelEmpty = false;
            bool eventRepeatVLabelName = false;
            bool eventAnyNameEmpty = false;
            bool eventRepeatName = false;
            VGroupStart();

            EditorGUILayout.Space();
            DrawTitle2("Event");
            EditorGUILayout.Space();
            #region Event
            if (EditorGUILayout.PropertyField(serializedObject.FindProperty("blueprintEvent")))
            {
                EditorGUI.indentLevel++;
                SerializedProperty sp = serializedObject.FindProperty("blueprintEvent");
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("startEvent"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("updateEvent"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("fixedUpdateEvent"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onDestroyEvent"));
                EditorGUILayout.Space();
                sp = sp.FindPropertyRelative("physicsEvent");
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onCollisionEnter"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onCollisionExit"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onCollisionStay"));

                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onCollisionEnter2D"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onCollisionExit2D"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onCollisionStay2D"));

                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onTriggerEnter"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onTriggerExit"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onTriggerStay"));

                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onTriggerEnter2D"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onTriggerExit2D"));
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("onTriggerStay2D"));
                EditorGUI.indentLevel--;
            }
            #endregion
            EditorGUILayout.Space();
            DrawTitle2("Custom Event");
            EditorGUILayout.Space();
            #region Custom Event
            VGroupStart();
            for(int i = 0; i < b.blueprintEvent.customEvent.Count; i++)
            {
                if (b.blueprintEvent.customEvent[i].eventName == null || b.blueprintEvent.customEvent[i].eventName == "")
                {
                    eventAnyNameEmpty = true;
                }
                for (int j = i + 1; j < b.blueprintEvent.customEvent.Count; j++)
                {
                    if (b.blueprintEvent.customEvent[i].eventName == b.blueprintEvent.customEvent[j].eventName)
                    {
                        eventRepeatName = true;
                    }
                }
                b.blueprintEvent.customEvent[i].fold = EditorGUILayout.Foldout(b.blueprintEvent.customEvent[i].fold, b.blueprintEvent.customEvent[i].eventName);
                if (!b.blueprintEvent.customEvent[i].fold) continue;
                VGroupStart();
                b.blueprintEvent.customEvent[i].eventName = 
                    EditorGUILayout.TextField("Event Name", b.blueprintEvent.customEvent[i].eventName);
                b.blueprintEvent.customEvent[i].returnType =
                    (FieldType)EditorGUILayout.EnumPopup("Return Type", b.blueprintEvent.customEvent[i].returnType);
                #region Arugment
                HGroupStart();
                EditorGUILayout.LabelField("Label", GUILayout.MinWidth(100));
                EditorGUILayout.LabelField("Type", GUILayout.MinWidth(100));
                HGroupEnd();

                for (int j = 0; j < b.blueprintEvent.customEvent[i].arugments.Count; j++)
                {
                    BlueprintVariable bmv = b.blueprintEvent.customEvent[i].arugments[j];
                    HGroupStart();
                    bmv.label = EditorGUILayout.TextField(bmv.label);
                    EditorGUI.BeginChangeCheck();
                    bmv.type = (FieldType)EditorGUILayout.EnumPopup(bmv.type);
                    if (EditorGUI.EndChangeCheck())
                    {
                        b.ChangeCustomEventArugment(i, j);
                    }
                    if (GUILayout.Button("-"))
                    {
                        b.DeleteCustomEventArugment(i, j);
                        b.blueprintEvent.customEvent[i].arugments.RemoveAt(j);
                        return;
                    }
                    if (b.blueprintEvent.customEvent[i].arugments[j].label == "" ||
                        b.blueprintEvent.customEvent[i].arugments[j].label == null)
                        eventAnyArugmentLabelEmpty = true;
                    for(int k = j + 1; k < b.blueprintEvent.customEvent[i].arugments.Count; k++)
                    {
                        if(b.blueprintEvent.customEvent[i].arugments[j].label ==
                            b.blueprintEvent.customEvent[i].arugments[k].label)
                        {
                            eventRepeatVLabelName = true;
                        }
                    }
                    HGroupEnd();
                }
                if (eventAnyArugmentLabelEmpty)
                {
                    EditorGUILayout.HelpBox("Arugment label cannot be empty", MessageType.Error);
                }
                if (eventRepeatVLabelName)
                {
                    EditorGUILayout.HelpBox("Arugment label cannot be repeat", MessageType.Error);
                }
                HGroupStart();
                if (GUILayout.Button("Add Arugment"))
                {
                    b.blueprintEvent.customEvent[i].arugments.Add(new BlueprintVariable());
                }
                if (GUILayout.Button("Clear Arugment"))
                {
                    b.blueprintEvent.customEvent[i].arugments.Clear();
                }
                if (GUILayout.Button("Delete Custom Event"))
                {
                    b.DeleteCustomEvent(i);
                    b.blueprintEvent.customEvent.RemoveAt(i);
                    return;
                }
                HGroupEnd();
                VGroupEnd();
                EditorGUILayout.Space();
                #endregion
            }
            HGroupStart();
            if (GUILayout.Button("Add Custom Event"))
            {
                b.blueprintEvent.customEvent.Add(new BlueprintCustomEvent());
            }
            if (GUILayout.Button("Clear Custom Event"))
            {
                b.blueprintEvent.customEvent.Clear();
            }
            HGroupEnd();
            if (eventAnyNameEmpty)
            {
                EditorGUILayout.HelpBox("Event Name cannot be empty", MessageType.Error);
            }
            if (eventRepeatName)
            {
                EditorGUILayout.HelpBox("Event Name cannot be repeat", MessageType.Error);
            }
            VGroupEnd();
            #endregion
            EditorGUILayout.Space();
            DrawTitle2("Custom Variable");
            EditorGUILayout.Space();
            #region Custom Variable
            VGroupStart();
            HGroupStart();
            EditorGUILayout.LabelField("Label", GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Type", GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Default variable", GUILayout.MinWidth(100));
            HGroupEnd();
            for (int i = 0; i < b.blueprintVariables.Count; i++)
            {
                HGroupStart();
                b.blueprintVariables[i].label = EditorGUILayout.TextField(b.blueprintVariables[i].label);
                b.blueprintVariables[i].type = (FieldType)EditorGUILayout.EnumPopup(b.blueprintVariables[i].type);
                b.blueprintVariables[i].variable = DrawFieldHelper(b.blueprintVariables[i].variable, Field.GetTypeByFieldType(b.blueprintVariables[i].type).FullName);
                if (GUILayout.Button("-"))
                {
                    b.blueprintVariables.RemoveAt(i);
                }
                if (b.blueprintVariables[i].label == "" || b.blueprintVariables[i].label == null) anyVariableLabelEmpty = true;

                bool repeat = false;
                for(int k = i + 1; k < b.blueprintVariables.Count; k++)
                {
                    if(b.blueprintVariables[i].label == b.blueprintVariables[k].label)
                    {
                        repeat = true;
                    }
                }
                if (repeat) repeatVLabelName = true;
                HGroupEnd();
            }
            HGroupStart();
            if (GUILayout.Button("Add Custom Variable"))
            {
                b.blueprintVariables.Add(new BlueprintVariable());
            }
            if (GUILayout.Button("Clear Custom Variable"))
            {
                b.blueprintVariables.Clear();
            }
            HGroupEnd();
            if (anyVariableLabelEmpty)
            {
                EditorGUILayout.HelpBox("Variable label cannot be empty", MessageType.Error);
            }
            if (repeatVLabelName)
            {
                EditorGUILayout.HelpBox("Variable label cannot be repeat", MessageType.Error);
            }
            VGroupEnd();
            #endregion
            EditorGUILayout.Space(); EditorGUILayout.Space();
            EditorGUILayout.Space();
            DrawTitle2("Total");
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Node", b.nodes.Count.ToString());
            EditorGUILayout.LabelField("Connection", b.connections.Count.ToString());
            VGroupEnd();
        }
    }
}

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
            EditorGUI.BeginChangeCheck();
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
            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(b);
            #endregion
            EditorGUILayout.Space();
            DrawTitle2("Custom Event");
            EditorGUILayout.Space();
            #region Custom Event
            VGroupStart();
            /* Private custom event */
            EditorGUI.BeginChangeCheck();
            {
                DrawTitle3("Blueprint Custom Event");
                for (int i = 0; i < b.blueprintEvent.customEvent.Count; i++)
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
                    b.blueprintEvent.customEvent[i].accessAbility =
                        (AccessAbility)EditorGUILayout.EnumPopup("Access Ability", b.blueprintEvent.customEvent[i].accessAbility);
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
                        for (int k = j + 1; k < b.blueprintEvent.customEvent[i].arugments.Count; k++)
                        {
                            if (b.blueprintEvent.customEvent[i].arugments[j].label ==
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
            }
            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(b);
            if (b.Inherit != null)
            {
                DrawTitle3("Inherit Custom Event");
                EditorGUILayout.Space();
                List<BlueprintCustomEvent> bufferC = b.GetInheritEvent();
                foreach (var i in b.blueprintEvent.customEvent)
                {
                    bufferC.Remove(i);
                }
                for (int i = 0; i < bufferC.Count; i++)
                {
                    BlueprintCustomEvent targetC = bufferC[i];
                    targetC.fold = EditorGUILayout.Foldout(targetC.fold, targetC.eventName);
                    if (targetC.fold)
                    {
                        GUI.enabled = false;
                        targetC.eventName =
                            EditorGUILayout.TextField("Event Name", targetC.eventName);
                        targetC.accessAbility =
                            (AccessAbility)EditorGUILayout.EnumPopup("Access Ability", targetC.accessAbility);
                        targetC.returnType =
                            (FieldType)EditorGUILayout.EnumPopup("Return Type", targetC.returnType);

                        HGroupStart();
                        EditorGUILayout.LabelField("Label", GUILayout.MinWidth(100));
                        EditorGUILayout.LabelField("Type", GUILayout.MinWidth(100));
                        HGroupEnd();
                        for (int j = 0; j < targetC.arugments.Count; j++)
                        {
                            HGroupStart();
                            BlueprintVariable bufferCV = targetC.arugments[j];
                            bufferCV.label = EditorGUILayout.TextField(bufferCV.label);
                            bufferCV.type = (FieldType)EditorGUILayout.EnumPopup(bufferCV.type);
                            HGroupEnd();
                        }
                        GUI.enabled = true;
                    }
                }
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
            EditorGUILayout.LabelField("Access", GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Label", GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Type", GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Default variable/Size", GUILayout.MinWidth(100));
            HGroupEnd();
            /* Private custom variable */
            EditorGUI.BeginChangeCheck();
            {
                for (int i = 0; i < b.blueprintVariables.Count; i++)
                {
                    HGroupStart();
                    b.blueprintVariables[i].fieldContainer = (FieldContainer)EditorGUILayout.EnumPopup(b.blueprintVariables[i].fieldContainer);
                    HGroupEnd();
                    HGroupStart();
                    b.blueprintVariables[i].accessAbility = (AccessAbility)EditorGUILayout.EnumPopup(b.blueprintVariables[i].accessAbility);
                    b.blueprintVariables[i].label = EditorGUILayout.TextField(b.blueprintVariables[i].label);
                    EditorGUI.BeginChangeCheck();
                    b.blueprintVariables[i].type = (FieldType)EditorGUILayout.EnumPopup(b.blueprintVariables[i].type);
                    if (EditorGUI.EndChangeCheck())
                    {
                        b.ChangeCustomVariableType(i);
                    }

                    if (b.blueprintVariables[i].fieldContainer == FieldContainer.Object)
                        b.blueprintVariables[i].variable = Field.DrawFieldHelper(b.blueprintVariables[i].variable, b.blueprintVariables[i].type);
                    else
                        b.blueprintVariables[i].variable.genericBasicType.target_Int = EditorGUILayout.IntField(b.blueprintVariables[i].variable.genericBasicType.target_Int);

                    if (GUILayout.Button("-"))
                    {
                        b.DeleteCustomVariable(i);
                        b.blueprintVariables.RemoveAt(i);
                        continue;
                    }
                    if (b.blueprintVariables[i].label == "" || b.blueprintVariables[i].label == null) anyVariableLabelEmpty = true;
                    bool repeat = false;
                    for (int k = i + 1; k < b.blueprintVariables.Count; k++)
                    {
                        if (b.blueprintVariables[i].label == b.blueprintVariables[k].label)
                        {
                            repeat = true;
                        }
                    }
                    if (repeat) repeatVLabelName = true;
                    HGroupEnd();
                }
            }
            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(b);

            /* Inherit custom variable */
            if (b.Inherit != null)
            {
                EditorGUI.BeginChangeCheck();
                {
                    List<BlueprintVariable> bufferV = b.GetInheritVariable();
                    foreach (var i in b.blueprintVariables)
                    {
                        bufferV.Remove(i);
                    }

                    for (int i = 0; i < bufferV.Count; i++)
                    {
                        HGroupStart();
                        BlueprintVariable variable = bufferV[i];
                        if (variable.accessAbility == AccessAbility.Private) continue;
                        GUI.enabled = false;
                        variable.accessAbility = (AccessAbility)EditorGUILayout.EnumPopup(variable.accessAbility);
                        variable.label = EditorGUILayout.TextField(variable.label);
                        variable.type = (FieldType)EditorGUILayout.EnumPopup(variable.type);
                        GUI.enabled = true;
                        variable.variable = Field.DrawFieldHelper(variable.variable, variable.type);
                        HGroupEnd();
                    }
                }
                if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(b.Inherit);
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
            EditorGUILayout.Space();
            DrawTitle2("Inherit");
            EditorGUILayout.Space();
            #region Inherit
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Inherit"));
            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(b);
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

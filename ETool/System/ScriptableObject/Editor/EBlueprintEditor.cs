#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    [CustomEditor(typeof(EBlueprint))]
    [CanEditMultipleObjects]
    public class EBlueprintEditor : EToolEditorBase
    {
        private EBlueprint b;

        private bool anyVariableLabelEmpty = false;
        private bool repeatVLabelName = false;
        private bool eventAnyArugmentLabelEmpty = false;
        private bool eventRepeatVLabelName = false;
        private bool eventAnyNameEmpty = false;
        private bool eventRepeatName = false;

        public override string GetInfoString()
        {
            return this.name;
        }

        public override void DrawEToolInformation()
        {
            b = (EBlueprint)target;

            anyVariableLabelEmpty = false;
            repeatVLabelName = false;
            eventAnyArugmentLabelEmpty = false;
            eventRepeatVLabelName = false;
            eventAnyNameEmpty = false;
            eventRepeatName = false;

            EditorGUI.BeginChangeCheck();
            VGroupStart();

            DrawEvent();
            DrawCustomEvent();
            DrawVariable();
            DrawInherit();

            EditorGUILayout.Space(); 
            EditorGUILayout.Space();

            DrawTotal();

            VGroupEnd();
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(this);
            }
        }

        private void DrawEvent()
        {
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
                EditorGUILayout.PropertyField(sp.FindPropertyRelative("lateUpdateEvent"));
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
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                b.Editor_ChangeEventToggleList();
                AssetDatabase.SaveAssets();
            }
            #endregion
        }

        private void DrawCustomEvent()
        {
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
                    #region Event Information
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

                    string buffer = b.Editor_CustomEvent_GetUniqueName_Field(EditorGUILayout.TextField("Event Name", b.blueprintEvent.customEvent[i].eventName), i);
                    if(b.blueprintEvent.customEvent[i].eventName != buffer)
                    {
                        b.Editor_CustomEvent_ChangeEventName(b.blueprintEvent.customEvent[i].eventName, buffer);
                        b.blueprintEvent.customEvent[i].eventName = buffer;
                    }

                    b.blueprintEvent.customEvent[i].accessAbility =
                        (AccessAbility)EditorGUILayout.EnumPopup("Access Ability", b.blueprintEvent.customEvent[i].accessAbility);

                    EditorGUI.BeginChangeCheck();
                    b.blueprintEvent.customEvent[i].returnType =
                        (FieldType)EditorGUILayout.EnumPopup("Return Type", b.blueprintEvent.customEvent[i].returnType);
                    b.blueprintEvent.customEvent[i].returnContainer =
                        (FieldContainer)EditorGUILayout.EnumPopup("Return Container", b.blueprintEvent.customEvent[i].returnContainer);
                    if (EditorGUI.EndChangeCheck()) // Change return information
                        b.Editor_ChangeCustomEventReturnTypeOrContainer(i);
                    #endregion

                    #region Arugment
                    HGroupStart();
                    EditorGUILayout.LabelField("Label", GUILayout.MinWidth(100));
                    EditorGUILayout.LabelField("Type", GUILayout.MinWidth(100));
                    EditorGUILayout.LabelField("Container", GUILayout.MinWidth(100));
                    HGroupEnd();

                    /* Render argument information */
                    for (int j = 0; j < b.blueprintEvent.customEvent[i].arugments.Count; j++)
                    {
                        BlueprintVariable bmv = b.blueprintEvent.customEvent[i].arugments[j];
                        HGroupStart();
                        bmv.label = b.Editor_CustomEventArgument_GetUniqueName_Field(EditorGUILayout.TextField(bmv.label), j, i);
                        EditorGUI.BeginChangeCheck();
                        bmv.type = (FieldType)EditorGUILayout.EnumPopup(bmv.type);
                        bmv.fieldContainer = (FieldContainer)EditorGUILayout.EnumPopup(bmv.fieldContainer);
                        if (EditorGUI.EndChangeCheck())
                        {
                            b.Editor_CustomEventArgument_ChangeCustomEventArugment(i);
                        }
                        if (GUILayout.Button("-"))
                        {
                            b.Editor_CustomEventArgument_ChangeCustomEventArugment(i);
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
                        b.Editor_CustomEventArgument_AddArgument(i);
                        return;
                    }
                    if (GUILayout.Button("Clear Arugment"))
                    {
                        b.Editor_CustomEventArgument_CleanArgument(i);
                        return;
                    }
                    if (GUILayout.Button("Delete Custom Event"))
                    {
                        b.Editor_CustomEvent_DeleteCustomEvent(i);
                        return;
                    }
                    HGroupEnd();
                    VGroupEnd();
                    EditorGUILayout.Space();
                    #endregion
                }
            }
            if (EditorGUI.EndChangeCheck()) AssetDatabase.SaveAssets();
            /* Inherit event */
            if (b.Inherit != null)
            {
                DrawTitle3("Inherit Custom Event");
                EditorGUILayout.Space();
                List<Tuple<BlueprintCustomEvent, EBlueprint>> bufferC = b.GetInheritEvent();
                
                for (int i = 0; i < bufferC.Count; i++)
                {
                    Tuple<BlueprintCustomEvent, EBlueprint> targetC = bufferC[i];
                    targetC.Item1.fold = EditorGUILayout.Foldout(targetC.Item1.fold, targetC.Item1.eventName);
                    if (targetC.Item1.fold)
                    {
                        GUI.enabled = false;
                        targetC.Item1.eventName =
                            EditorGUILayout.TextField("Event Name", targetC.Item1.eventName);
                        targetC.Item1.accessAbility =
                            (AccessAbility)EditorGUILayout.EnumPopup("Access Ability", targetC.Item1.accessAbility);
                        targetC.Item1.returnType =
                            (FieldType)EditorGUILayout.EnumPopup("Return Type", targetC.Item1.returnType);
                        targetC.Item1.returnContainer =
                            (FieldContainer)EditorGUILayout.EnumPopup("Return Container", targetC.Item1.returnContainer);
                        HGroupStart();
                        EditorGUILayout.LabelField("Label", GUILayout.MinWidth(100));
                        EditorGUILayout.LabelField("Type", GUILayout.MinWidth(100));
                        HGroupEnd();
                        for (int j = 0; j < targetC.Item1.arugments.Count; j++)
                        {
                            HGroupStart();
                            BlueprintVariable bufferCV = targetC.Item1.arugments[j];
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
                b.Editor_CustomEvent_AddCustomEvent();
                return;
            }
            if (GUILayout.Button("Clear Custom Event"))
            {
                b.Editor_CustomEvent_CleanCustomEvent();
                return;
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
        }

        private void DrawVariable()
        {
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
                    /* Begin check the var header state */
                    EditorGUI.BeginChangeCheck();
                    HGroupStart();
                    b.blueprintVariables[i].fieldContainer = (FieldContainer)EditorGUILayout.EnumPopup(b.blueprintVariables[i].fieldContainer);
                    HGroupEnd();
                    HGroupStart();
                    b.blueprintVariables[i].accessAbility = (AccessAbility)EditorGUILayout.EnumPopup(b.blueprintVariables[i].accessAbility);

                    string buffer = b.Editor_CustomVariable_GetUniqueName_Field(EditorGUILayout.TextField(b.blueprintVariables[i].label), i);
                    if(b.blueprintVariables[i].label != buffer)
                    {
                        b.Editor_CustomVariable_ChangeCustomVariableName(b.blueprintVariables[i].label, buffer);
                        b.blueprintVariables[i].label = buffer;
                    }
                    b.blueprintVariables[i].type = (FieldType)EditorGUILayout.EnumPopup(b.blueprintVariables[i].type);
                    if (EditorGUI.EndChangeCheck())
                    {
                        /* If change state is true, calling change event */
                        b.Editor_CustomVariable_ChangeCustomVariable(b.blueprintVariables[i].label);
                    }

                    if (b.blueprintVariables[i].fieldContainer == FieldContainer.Object)
                        b.blueprintVariables[i].variable = Field.DrawFieldHelper(b.blueprintVariables[i].variable, b.blueprintVariables[i].type);
                    else
                        b.blueprintVariables[i].variable.genericBasicType.target_Int = EditorGUILayout.IntField(b.blueprintVariables[i].variable.genericBasicType.target_Int);

                    if (GUILayout.Button("-"))
                    {
                        b.Editor_CustomVariable_DeleteCustomVariable(i);                        
                        return;
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
            if (EditorGUI.EndChangeCheck()) AssetDatabase.SaveAssets();

            /* Inherit custom variable */
            if (b.Inherit != null)
            {
                EditorGUI.BeginChangeCheck();
                {
                    List<Tuple<BlueprintVariable, EBlueprint>> bufferV = b.GetInheritVariable();
                    List<Tuple<BlueprintVariable, EBlueprint>> DeleteBuffer = new List<Tuple<BlueprintVariable, EBlueprint>>();
                    foreach (var i in bufferV)
                    {
                        if(i.Item2 == b)
                        {
                            DeleteBuffer.Add(i);
                        }
                    }
                    for(int i = 0; i < DeleteBuffer.Count; i++)
                    {
                        bufferV.Remove(DeleteBuffer[i]);
                    }

                    for (int i = 0; i < bufferV.Count; i++)
                    {
                        HGroupStart();
                        BlueprintVariable variable = bufferV[i].Item1;
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
                if (EditorGUI.EndChangeCheck()) AssetDatabase.SaveAssets();
            }
            HGroupStart();
            if (GUILayout.Button("Add Custom Variable"))
            {
                b.Editor_CustomVariable_AddCustomVariable();
                return;
            }
            if (GUILayout.Button("Clear Custom Variable"))
            {
                b.Editor_CustomVariable_CleanCustomVariable();
                return;
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
        }

        private void DrawInherit()
        {
            EditorGUILayout.Space();
            DrawTitle2("Inherit");
            EditorGUILayout.Space();
            #region Inherit
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Inherit"));
            if (EditorGUI.EndChangeCheck())
            {
                EBlueprint last = b.Inherit;
                serializedObject.ApplyModifiedProperties();
                b.Editor_InheritUpdate();
                AssetDatabase.SaveAssets();
            }
            #endregion
        }

        private void DrawTotal()
        {
            EditorGUILayout.Space();
            DrawTitle2("Total");
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Node", b.nodes.Count.ToString());
            EditorGUILayout.LabelField("Connection", b.connections.Count.ToString());
        }
    }
}
#endif
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Variable/Set External Variable")]
    [ETool_Menu("Variable")]
    public class SetOutterVariable : NodeBase
    {
        private List<Tuple<BlueprintVariable, EBlueprint>> MyTarget;
        private object ObjBuffer;

        public SetOutterVariable(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set External Variable";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            EBlueprint target = (EBlueprint)GetFieldOrLastInputField(2, data);
            string label = (string)fields[3].GetArrayValue((int)fields[3].GetValue(FieldType.Int), FieldType.String);
            target.Custom_CallSetVariable(label, fields[4].fieldType, GetFieldOrLastInputField(4, data));
            ActiveNextEvent(0, data);
        }

        /* System should give the items for this node to select */
        public void SetOptions(List<Tuple<BlueprintVariable, EBlueprint>> options)
        {
            MyTarget = options;

            fields[3].target_array = new GenericObject[options.Count];
            for (int i = 0; i < options.Count; i++)
            {
                fields[3].target_array[i] = new GenericObject();
                fields[3].target_array[i].genericBasicType.target_String = options[i].Item1.label;
            }

            if (fields[3].target.genericBasicType.target_Int < 0 || fields[3].target.genericBasicType.target_Int + 1 > options.Count)
            {
                fields[3].target.genericBasicType.target_Int = 0;
                EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[4]);
            }

            GetFieldDone();
        }

        public void RenameContent(string oldName, string newName)
        {
            foreach (var i in fields[3].target_array)
            {
                if (i.genericBasicType.target_String == oldName)
                {
                    i.genericBasicType.target_String = newName;
                }
            }
        }

        [NodePropertyGet(typeof(object), 4)]
        public object GetObj(BlueprintInput data)
        {
            EBlueprint target = (EBlueprint)GetFieldOrLastInputField(2, data);
            string label = (string)fields[3].GetArrayValue((int)fields[3].GetValue(FieldType.Int), FieldType.String);
            return target.Custom_CallGetVariable(label, fields[4].fieldType);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "BP Sample", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "BP Instance", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Dropdown, "BP Varaible", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Event, "None", ConnectionType.None, this, FieldContainer.Object));
        }

        public override void FieldUpdate()
        {
            EBlueprint e = (EBlueprint)fields[1].GetValue(FieldType.Blueprint);

            if(e == null)
            {
                Zero();
                return;
            }
            else
            {
                MyTarget = e.GetInheritVariable();
            }

            SetOptions(MyTarget);
        }

        private void GetFieldDone()
        {
            if (MyTarget.Count == 0) return;
            int index = fields[3].target.genericBasicType.target_Int;
            targetEventOrVar = MyTarget[index].Item2.name + "." + fields[3].target_array[index].genericBasicType.target_String;

            BlueprintVariable target = MyTarget[index].Item1;

            if (fields[4].fieldType != target.type || fields[4].fieldContainer != target.fieldContainer)
            {
                fields[4] = new Field(target.type, "Value", ConnectionType.DataBoth, true, this, target.fieldContainer);
                EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[4]);
            }

            targetEventOrVar = MyTarget[index].Item2.name + "." + MyTarget[index].Item1.label;
        }

        private void Zero()
        {
            fields[4] = new Field(FieldType.Event, "None", ConnectionType.None, this, FieldContainer.Object);

            fields[3].target_array = new GenericObject[0];
            fields[3].target.genericBasicType.target_Int = 0;
        }
    }
}

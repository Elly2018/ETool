using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Variable/Set Variable")]
    [ETool_Menu("Variable")]
    public class SetVariable : NodeBase
    {
        private List<Tuple<BlueprintVariable, EBlueprint>> MyTarget;

        public SetVariable(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Variable";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            if (fields[2].fieldContainer == FieldContainer.Object)
            {
                object o = GetFieldOrLastInputField<object>(2, data);
                if(o != null)
                {
                    SetVariable(data, fields[2].fieldType, o);
                }
            }
            else
            {
                object[] o = GetFieldOrLastInputField<object[]>(2, data);
                if(o != null)
                {
                    SetVariableArray(data, fields[2].fieldType, o);
                }
            }
            ActiveNextEvent(0, data);
        }

        public void SetOptions(List<Tuple<BlueprintVariable, EBlueprint>> options)
        {
            MyTarget = options;

            fields[1].target_array = new GenericObject[options.Count];
            for (int i = 0; i < options.Count; i++)
            {
                fields[1].target_array[i] = new GenericObject();
                fields[1].target_array[i].genericBasicType.target_String = options[i].Item1.label;
            }

            if (fields[1].target.genericBasicType.target_Int < 0 || fields[1].target.genericBasicType.target_Int + 1 > options.Count)
            {
                fields[1].target.genericBasicType.target_Int = 0;
                EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[1]);
            }

            GetFieldDone();
        }

        public void RenameContent(string oldName, string newName)
        {
            foreach(var i in fields[1].target_array)
            {
                if(i.genericBasicType.target_String == oldName)
                {
                    i.genericBasicType.target_String = newName;
                }
            }
        }

        public override void FieldUpdate()
        {
            EBlueprint e = EBlueprint.GetBlueprintByNode(this);

            SetOptions(e.GetInheritVariable());
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Dropdown, "Variable", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Value", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        private void GetFieldDone()
        {
            if (MyTarget.Count == 0) return;
            int index = fields[1].target.genericBasicType.target_Int;
            targetEventOrVar = MyTarget[index].Item2.name + "." + fields[1].target_array[index].genericBasicType.target_String;

            BlueprintVariable target = MyTarget[index].Item1;

            if (fields[2].fieldType != target.type || fields[2].fieldContainer != target.fieldContainer)
            {
                fields[2] = new Field(target.type, "Value", ConnectionType.DataBoth, true, this, target.fieldContainer);
                EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[1]);
            }

            targetEventOrVar = MyTarget[index].Item2.name + "." + MyTarget[index].Item1.label;
        }

        [NodePropertyGet(typeof(object), 2)]
        public object GetMyVariable(BlueprintInput data)
        {
            if(data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].fieldContainer == FieldContainer.Object)
            {
                return Field.GetObjectByFieldType(
                data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].type,
                data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].variable);
            }
            else
            {
                return Field.GetObjectArrayByFieldType(
                data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].type,
                data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].variable_Array);
            }
        }
    }
}

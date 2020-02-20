using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Variable/Get Variable")]
    [ETool_Menu("Variable")]
    public class GetVariable : NodeBase
    {
        private List<Tuple<BlueprintVariable, EBlueprint>> MyTarget;

        public GetVariable(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Variable";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Dropdown, "Variable", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        public void RenameContent(string oldName, string newName)
        {
            foreach (var i in fields[0].target_array)
            {
                if (i.genericBasicType.target_String == oldName)
                {
                    i.genericBasicType.target_String = newName;
                }
            }
        }

        /* System should give the items for this node to select */
        public void SetOptions(List<Tuple<BlueprintVariable, EBlueprint>> options)
        {
            MyTarget = options;

            fields[0].target_array = new GenericObject[options.Count];
            for (int i = 0; i < options.Count; i++)
            {
                fields[0].target_array[i] = new GenericObject();
                fields[0].target_array[i].genericBasicType.target_String = options[i].Item1.label;
            }

            if(fields[0].target.genericBasicType.target_Int < 0 || fields[0].target.genericBasicType.target_Int + 1 > options.Count)
            {
                fields[0].target.genericBasicType.target_Int = 0;
                EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[1]);
            }

            GetFieldDone();
        }

        public override void FieldUpdate()
        {
            EBlueprint e = EBlueprint.GetBlueprintByNode(this);

            SetOptions(e.GetInheritVariable());
        }

        private void GetFieldDone()
        {
            if (MyTarget.Count == 0) return;
            int index = fields[0].target.genericBasicType.target_Int;
            targetEventOrVar = MyTarget[index].Item2.name + "." + fields[0].target_array[index].genericBasicType.target_String;

            BlueprintVariable target = MyTarget[index].Item1;

            if(fields[1].fieldType != target.type || fields[1].fieldContainer != target.fieldContainer)
            {
                fields[1] = new Field(target.type, "Result", ConnectionType.DataOutput, true, this, target.fieldContainer);
                EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[1]);
            }

            targetEventOrVar = MyTarget[index].Item2.name + "." + MyTarget[index].Item1.label;
        }

        [NodePropertyGet(typeof(object), 1)]
        public object GetMyVariable(BlueprintInput data)
        {
            if(fields[1].fieldContainer == FieldContainer.Object)
            {
                return GetVarialbe(data, fields[1].fieldType);
            }
            else
            {
                return GetVarialbeArray(data, fields[1].fieldType);
            }
        }
    }
}

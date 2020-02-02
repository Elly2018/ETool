using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Variable/Set Variable")]
    public class SetVariable : NodeBase
    {
        public SetVariable(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Variable";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            if (fields.Count == 3)
            {
                if(data.blueprintVariables[(Int32)fields[1].GetValue(FieldType.Int)].fieldContainer == FieldContainer.Object)
                {
                    object o = GetFieldOrLastInputField<object>(2, data);
                    Field.SetObjectByFieldType(fields[2].fieldType, data.blueprintVariables[(Int32)fields[1].GetValue(FieldType.Int)].variable, o);
                }
                else
                {
                    object[] o = GetFieldOrLastInputField<object[]>(2, data);

                    data.blueprintVariables[(Int32)fields[1].GetValue(FieldType.Int)].variable_Array = 
                        Field.SetObjectArrayByField(fields[2].fieldType, data.blueprintVariables[(Int32)fields[1].GetValue(FieldType.Int)].variable_Array, o);
                }
            }

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Variable, "Target", ConnectionType.None, this, FieldContainer.Object));
        }

        public override void PostFieldInitialize()
        {
            GetFieldDone();
        }

        public override void PostFieldInitialize(BlueprintInput data)
        {
            GetFieldDone(data);
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            GetFieldDone(data);
        }

        public override void FieldUpdate()
        {
            GetFieldDone();
        }

        private void GetFieldDone()
        {
            List<BlueprintVariable> bv = NodeBasedEditor.Instance.GetAllCustomVariable();

            if(fields.Count == 2)
            {
                fields.Add(new Field(bv[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].type,
                    "Result",
                    ConnectionType.DataBoth,
                    this, bv[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].fieldContainer));
            }
            else if(fields.Count == 3)
            {
                if (bv[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].type != fields[2].fieldType)
                {
                    NodeBasedEditor.Instance.RemoveRelateConnectionInField(fields[2]);
                    fields[2] = new Field(bv[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].type,
                        "Result",
                        ConnectionType.DataBoth,
                        this, bv[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].fieldContainer);
                }
            }

        }

        private void GetFieldDone(BlueprintInput data)
        {
            if (fields.Count == 2)
            {
                fields.Add(new Field(data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].type,
                    "Result",
                    ConnectionType.DataBoth,
                    this, data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].fieldContainer));
            }
            else if (fields.Count == 3)
            {
                if (data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].type != fields[2].fieldType)
                {
                    NodeBasedEditor.Instance.RemoveRelateConnectionInField(fields[2]);
                    fields[2] = new Field(data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int,
                        fields[1].target)].type,
                        "Result",
                        ConnectionType.DataBoth,
                        this, data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[1].target)].fieldContainer);
                }
            }

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

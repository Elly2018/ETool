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
            title = "Set Variable";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            if (fields.Count == 3)
            {
                object o = GetFieldOrLastInputField(2, data);
                Field.SetObjectByFieldType(fields[2].fieldType, data.blueprintVariables[fields[1].target.target_Int].variable, o);
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

        public override void FieldUpdate()
        {
            GetFieldDone();
        }

        private void GetFieldDone()
        {
            List<BlueprintVariable> bv = NodeBasedEditor.Instance.GetAllCustomVariable();

            if(fields.Count == 2)
            {
                fields.Add(new Field(bv[fields[1].target.target_Int].type, "Result", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            }
            else if(fields.Count == 3)
            {
                if (bv[fields[1].target.target_Int].type != fields[2].fieldType)
                {
                    NodeBasedEditor.Instance.RemoveRelateConnectionInField(fields[2]);
                    fields[2] = new Field(bv[fields[1].target.target_Int].type, "Result", ConnectionType.DataBoth, true, this, FieldContainer.Object);
                }
            }

        }

        private void GetFieldDone(BlueprintInput data)
        {
            if (fields.Count == 2)
            {
                fields.Add(new Field(data.blueprintVariables[fields[1].target.target_Int].type, "Result", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            }
            else if (fields.Count == 3)
            {
                if (data.blueprintVariables[fields[1].target.target_Int].type != fields[2].fieldType)
                {
                    NodeBasedEditor.Instance.RemoveRelateConnectionInField(fields[2]);
                    fields[2] = new Field(data.blueprintVariables[fields[1].target.target_Int].type, "Result", ConnectionType.DataBoth, true, this, FieldContainer.Object);
                }
            }

        }

        [NodePropertyGet(typeof(object), 2)]
        public object GetMyVariable(BlueprintInput data)
        {
            return Field.GetObjectByFieldType(data.blueprintVariables[fields[1].target.target_Int].type, data.blueprintVariables[fields[1].target.target_Int].variable);
        }
    }
}

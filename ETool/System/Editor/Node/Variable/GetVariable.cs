using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Variable/Get Variable")]
    public class GetVariable : NodeBase
    {
        public GetVariable(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Variable";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Variable, "Variable", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
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
            try
            {
                NodeBasedEditor.Instance.RemoveRelateConnectionInField(fields[1]);
                fields[1] = new Field(bv[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[0].target)].type, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object);
            }
            catch { }
        }

        private void GetFieldDone(BlueprintInput data)
        {
            try
            {
                fields[1] = new Field(data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[0].target)].type, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object);
            }
            catch { }
        }

        [NodePropertyGet(typeof(object), 1)]
        public object GetMyVariable(BlueprintInput data)
        {
            return Field.GetObjectByFieldType(data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[0].target)].type, data.blueprintVariables[(Int32)Field.GetObjectByFieldType(FieldType.Int, fields[0].target)].variable);
        }
    }
}

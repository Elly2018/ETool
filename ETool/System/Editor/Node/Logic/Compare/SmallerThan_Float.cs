using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Compare/Float < Float")]
    public class SmallerThan_Float : NodeBase
    {
        public SmallerThan_Float(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Float A < Float B";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Int A", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Int B", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 2)]
        public bool GetResult(BlueprintInput data)
        {
            return (int)GetFieldOrLastInputField(0, data) < (int)GetFieldOrLastInputField(1, data);
        }
    }
}

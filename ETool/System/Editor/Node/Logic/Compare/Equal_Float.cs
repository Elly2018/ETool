using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Compare/Float = Float")]
    public class Equal_Float : NodeBase
    {
        public Equal_Float(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Equal";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "object A", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "object B", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 2)]
        public bool GetResult(BlueprintInput data)
        {
            return (float)GetFieldOrLastInputField(0, data) == (float)GetFieldOrLastInputField(1, data);
        }
    }
}

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Compare/Int = Int")]
    public class Equal_Int : NodeBase
    {
        public Equal_Int(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Equal";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "object A", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "object B", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 2)]
        public bool GetResult(BlueprintInput data)
        {
            return (int)GetFieldOrLastInputField(0, data) == (int)GetFieldOrLastInputField(1, data);
        }
    }
}

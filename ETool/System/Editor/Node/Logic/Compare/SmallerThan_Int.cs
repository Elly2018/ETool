using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Compare/Int < Int")]
    public class SmallerThan_Int : NodeBase
    {
        public SmallerThan_Int(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Int A < Int B";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Int A", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Int B", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 2)]
        public bool GetResult(BlueprintInput data)
        {
            return (int)GetFieldOrLastInputField(0, data) < (int)GetFieldOrLastInputField(1, data);
        }
    }
}

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Float/=>Long")]
    public class FloatToLong : NodeBase
    {
        public FloatToLong(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Float => Long";
        }

        [NodePropertyGet(typeof(long), 1)]
        public long GetString(BlueprintInput data)
        {
            return (long)((float)GetFieldOrLastInputField(0, data));
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Float", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Long, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}


using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Int/=>Long")]
    public class IntToLong : NodeBase
    {
        public IntToLong(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Int => Long";
        }

        [NodePropertyGet(typeof(long), 1)]
        public long GetString(BlueprintInput data)
        {
            return (long)((int)GetFieldOrLastInputField(0, data));
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Int", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Long, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}



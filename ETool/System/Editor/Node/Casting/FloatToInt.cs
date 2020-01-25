using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Float/ => Int")]
    public class FloatToInt : NodeBase
    {
        public FloatToInt(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Float => String";
        }

        [NodePropertyGet(typeof(Int32), 1)]
        public int GetString(BlueprintInput data)
        {
            return (int)((float)GetFieldOrLastInputField(0, data));
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Float", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}


using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Float/ => String")]
    public class FloatToString : NodeBase
    {
        public FloatToString(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Float => String";
        }

        [NodePropertyGet(typeof(String), 1)]
        public string GetString(BlueprintInput data)
        {
            return ((float)GetFieldOrLastInputField(0, data)).ToString();
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Float", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}


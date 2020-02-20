using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Vector4/ => String")]
    public class Vector4ToString : NodeBase
    {
        public Vector4ToString(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector4 => String";
        }

        [NodePropertyGet(typeof(String), 1)]
        public String GetString(BlueprintInput data)
        {
            return ((Vector4)GetFieldOrLastInputField(0, data)).ToString();
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector4, "Vector4", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

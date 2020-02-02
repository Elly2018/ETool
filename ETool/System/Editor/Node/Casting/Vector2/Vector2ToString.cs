using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Vector2/ => String")]
    public class Vector2ToString : NodeBase
    {
        public Vector2ToString(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector2 => String";
        }

        [NodePropertyGet(typeof(String), 1)]
        public String GetString(BlueprintInput data)
        {
            return ((Vector2)GetFieldOrLastInputField(0, data)).ToString();
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector2, "Vector2", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Int/ => String")]
    public class IntToString : NodeBase
    {
        public IntToString(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Int => String";
        }

        [NodePropertyGet(typeof(String), 1)]
        public string GetString(BlueprintInput data)
        {
            return ((int)GetFieldOrLastInputField(0, data)).ToString();
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Int", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}


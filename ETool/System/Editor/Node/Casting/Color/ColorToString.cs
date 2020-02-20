using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Color/ColorToString")]
    [Casting_Menu("Color")]
    public class ColorToString : NodeBase
    {
        public ColorToString(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Color => String";
        }

        [NodePropertyGet(typeof(String), 1)]
        public string GetString(BlueprintInput data)
        {
            return ((Color)GetFieldOrLastInputField(0, data)).ToString();
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Color, "Color", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}


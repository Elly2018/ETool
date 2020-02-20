using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Color/Merge")]
    [Casting_Menu("Color")]
    public class Color_Merge : NodeBase
    {
        public Color_Merge(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Color";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Color, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "r", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "g", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "b", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "a", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Color), 0)]
        public Color GetResultVector3(BlueprintInput data)
        {
            return new Color(
                (float)GetFieldOrLastInputField(1, data),
                (float)GetFieldOrLastInputField(2, data),
                (float)GetFieldOrLastInputField(3, data),
                (float)GetFieldOrLastInputField(4, data));
        }
    }
}

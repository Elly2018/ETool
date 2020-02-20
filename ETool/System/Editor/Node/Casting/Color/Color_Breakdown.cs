using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Color/Breakdown")]
    [Casting_Menu("Color")]
    public class Color_Breakdown : NodeBase
    {
        public Color_Breakdown(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Color";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Color, "Result", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "r", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "g", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "b", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "a", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 1)]
        public float GetResultVector3x(BlueprintInput data)
        {
            return ((Color)GetFieldOrLastInputField(0, data)).r;
        }

        [NodePropertyGet(typeof(float), 2)]
        public float GetResultVector3y(BlueprintInput data)
        {
            return ((Color)GetFieldOrLastInputField(0, data)).g;
        }

        [NodePropertyGet(typeof(float), 3)]
        public float GetResultVector3z(BlueprintInput data)
        {
            return ((Color)GetFieldOrLastInputField(0, data)).b;
        }

        [NodePropertyGet(typeof(float), 4)]
        public float GetResultVector3a(BlueprintInput data)
        {
            return ((Color)GetFieldOrLastInputField(0, data)).a;
        }
    }
}

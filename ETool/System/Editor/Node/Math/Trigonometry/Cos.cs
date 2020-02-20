using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Trigonometry/Cos")]
    [Math_Menu("Trigonometry")]
    public class Cos : NodeBase
    {
        public Cos(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Cos";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Cos((float)GetFieldOrLastInputField(0, data));
        }
    }
}


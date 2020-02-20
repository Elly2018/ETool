using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Trigonometry/Tan")]
    [Math_Menu("Trigonometry")]
    public class Tan : NodeBase
    {
        public Tan(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Tan";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Tan((float)GetFieldOrLastInputField(0, data));
        }
    }
}


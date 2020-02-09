using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Trigonometry/Atan2")]
    public class Atan2 : NodeBase
    {
        public Atan2(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Atan2";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Y", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "X", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Atan2((float)GetFieldOrLastInputField(0, data), (float)GetFieldOrLastInputField(1, data));
        }
    }
}


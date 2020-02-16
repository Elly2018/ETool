using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/Repeat")]
    public class Repeat : NodeBase
    {
        public Repeat(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Repeat";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Length", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Repeat((float)GetFieldOrLastInputField(0, data), (float)GetFieldOrLastInputField(1, data));
        }
    }
}


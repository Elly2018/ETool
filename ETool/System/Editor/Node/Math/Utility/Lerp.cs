using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/Lerp")]
    public class Lerp : NodeBase
    {
        public Lerp(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Lerp";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Number A", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Number B", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Lerp((float)GetFieldOrLastInputField(0, data), (float)GetFieldOrLastInputField(1, data), (float)GetFieldOrLastInputField(2, data));
        }
    }
}


using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/DeltaAngle")]
    public class DeltaAngle : NodeBase
    {
        public DeltaAngle(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Delta Angle";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Current", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.DeltaAngle((float)GetFieldOrLastInputField(0, data), (float)GetFieldOrLastInputField(1, data));
        }
    }
}


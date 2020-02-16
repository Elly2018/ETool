using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/Max")]
    public class Max : NodeBase
    {
        public Max(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Max";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Number A", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Number B", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Max((float)GetFieldOrLastInputField(0, data), (float)GetFieldOrLastInputField(1, data));
        }
    }
}


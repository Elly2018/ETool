using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Absolute")]
    public class Absolute : NodeBase
    {
        public Absolute(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Absolute";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Abs((float)GetFieldOrLastInputField(0, data));
        }
    }
}


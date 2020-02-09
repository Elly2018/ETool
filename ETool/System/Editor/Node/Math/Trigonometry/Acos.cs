using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Trigonometry/Acos")]
    public class Acos : NodeBase
    {
        public Acos(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Acos";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Acos((float)GetFieldOrLastInputField(0, data));
        }
    }
}


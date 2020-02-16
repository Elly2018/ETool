using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/Sign")]
    public class Sign : NodeBase
    {
        public Sign(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Sign";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Sign((float)GetFieldOrLastInputField(0, data));
        }
    }
}


using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/Exp")]
    [Math_Menu("Utility")]
    public class Exp : NodeBase
    {
        public Exp(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Exp";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Exp((float)GetFieldOrLastInputField(0, data));
        }
    }
}


using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Trigonometry/Sin")]
    public class Sin : NodeBase
    {
        public Sin(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Sin";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Sin((float)GetFieldOrLastInputField(0, data));
        }
    }
}

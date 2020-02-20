using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/Clamp")]
    [Math_Menu("Utility")]
    public class Clamp : NodeBase
    {
        public Clamp(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Clamp";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Min", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Max", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Clamp((float)GetFieldOrLastInputField(0, data), (float)GetFieldOrLastInputField(1, data), (float)GetFieldOrLastInputField(2, data));
        }
    }
}



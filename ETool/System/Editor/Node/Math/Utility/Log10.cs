using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/Log10")]
    [Math_Menu("Utility")]
    public class Log10 : NodeBase
    {
        public Log10(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Log10";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Log10((float)GetFieldOrLastInputField(0, data));
        }
    }
}


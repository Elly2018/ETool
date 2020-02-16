using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/LogNature")]
    public class LogNature : NodeBase
    {
        public LogNature(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Nature Log";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Log((float)GetFieldOrLastInputField(0, data));
        }
    }
}


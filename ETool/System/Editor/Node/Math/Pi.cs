using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Pi")]
    public class Pi : NodeBase
    {
        public Pi(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Pi";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.PI;
        }
    }
}


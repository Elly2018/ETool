﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/Pi")]
    [Math_Menu("Utility")]
    public class Pi : NodeBase
    {
        public Pi(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Pi";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.PI;
        }
    }
}


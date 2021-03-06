﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Trigonometry/Atan")]
    [Math_Menu("Trigonometry")]
    public class Atan : NodeBase
    {
        public Atan(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Atan";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Atan((float)GetFieldOrLastInputField(0, data));
        }
    }
}


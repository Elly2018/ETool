﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/Pow")]
    [Math_Menu("Utility")]
    public class Pow : NodeBase
    {
        public Pow(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Pow";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Power", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Mathf.Pow((float)GetFieldOrLastInputField(0, data), (float)GetFieldOrLastInputField(1, data));
        }
    }
}


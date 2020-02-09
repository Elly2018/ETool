﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Vector/VectorDistance")]
    public class TwoVector2Distance : NodeBase
    {
        public TwoVector2Distance(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector Distance";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector2, "First", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector2, "Second", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            TypeUpdate();
        }

        public override void FieldUpdate()
        {
            TypeUpdate();
        }

        private void TypeUpdate()
        {

        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetResultVector2(BlueprintInput data)
        {
            return Vector2.Distance((Vector2)GetFieldOrLastInputField(1, data), (Vector2)GetFieldOrLastInputField(1, data));
        }
    }
}

﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Transform/Get Local Position")]
    public class GetLocalPosition : NodeBase
    {
        public GetLocalPosition(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get local Position";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 1)]
        public Vector3 GetString(BlueprintInput data)
        {
            Transform s0 = (Transform)GetFieldOrLastInputField(0, data);
            if (s0 == null) return Vector3.zero;
            return s0.localPosition;
        }
    }
}



﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Transform/Get Local Rotation")]
    public class GetLocalRotation : NodeBase
    {
        public GetLocalRotation(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Local Rotation";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Quaternion, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Quaternion), 1)]
        public Quaternion GetString(BlueprintInput data)
        {
            Transform s0 = (Transform)GetFieldOrLastInputField(0, data);
            if (s0 == null) return Quaternion.identity;
            return s0.localRotation;
        }
    }
}




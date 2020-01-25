﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Transform/Look At")]
    public class LookAt : NodeBase
    {
        public LookAt(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Look At";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Transform g1 = (Transform)GetFieldOrLastInputField(1, data);
            Transform g2 = (Transform)GetFieldOrLastInputField(2, data);
            if(g1 != null && g2 != null)
            {
                g2.LookAt(g1);
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Look At", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataOutput, this, FieldContainer.Object));
        }
    }
}




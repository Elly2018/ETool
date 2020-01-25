﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Transform/Rotate")]
    public class Rotate : NodeBase
    {
        public Rotate(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Rotate";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Vector3 g1 = (Vector3)GetFieldOrLastInputField(1, data);
            bool g2 = (bool)GetFieldOrLastInputField(2, data);
            Transform g3 = (Transform)GetFieldOrLastInputField(3, data);
            if (g3 != null)
            {
                if(g2)
                    g3.Rotate(g1, Space.Self);
                else
                    g3.Rotate(g1, Space.World);
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Rotate", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Local", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}




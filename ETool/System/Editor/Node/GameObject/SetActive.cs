using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Set Active")]
    public class SetActive : NodeBase
    {
        public SetActive(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Set Active";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ((GameObject)GetFieldOrLastInputField(2, data)).SetActive((bool)GetFieldOrLastInputField(1, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Active", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}



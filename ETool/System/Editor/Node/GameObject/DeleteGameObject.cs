using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Delete")]
    public class DeleteGameObject : NodeBase
    {
        public DeleteGameObject(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Delete GameObject";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GameObject.Destroy((GameObject)GetFieldOrLastInputField(1, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}



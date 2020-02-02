using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Cursor/Method/SetLockState")]
    public class CursorSetLockState : NodeBase
    {
        public CursorSetLockState(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Cursor Lockstate";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Cursor.lockState = (CursorLockMode)GetFieldOrLastInputField(1, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.CursorLockMode, "Lock Mode", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

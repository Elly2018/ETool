using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Cursor/Set Cursor Visible")]
    public class CursorSetVisible : NodeBase
    {
        public CursorSetVisible(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Cursor Visible";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Cursor.visible = (bool)GetFieldOrLastInputField(1, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Visible", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

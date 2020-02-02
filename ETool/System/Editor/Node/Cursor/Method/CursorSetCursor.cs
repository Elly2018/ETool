using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Cursor/Method/SetCursor")]
    public class CursorSetCursor : NodeBase
    {
        public CursorSetCursor(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Cursor";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Texture2D v1 = GetFieldOrLastInputField<Texture2D>(1, data);
            Vector2 v2 = GetFieldOrLastInputField<Vector2>(2, data);
            CursorMode v3 = GetFieldOrLastInputField<CursorMode>(3, data);
            Cursor.SetCursor(v1, v2, v3);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Texture2D, "Texture", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector2, "HotSpot", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.CursorMode, "CursorMode", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Cursor/Set Cursor")]
    public class CursorSetCursor : NodeBase
    {
        public CursorSetCursor(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Set Cursor";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            base.ProcessCalling(data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

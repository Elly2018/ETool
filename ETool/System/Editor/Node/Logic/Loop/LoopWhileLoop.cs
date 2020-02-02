using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Loop/While")]
    public class LoopWhileLoop : NodeBase
    {
        public LoopWhileLoop(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "While";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "True", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Condition", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Event, "Finish", ConnectionType.EventOutput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            while ((bool)GetFieldOrLastInputField(1, data))
            {
                ActiveNextEvent(0, data);
            }
            ActiveNextEvent(2, data);
        }
    }
}

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/If")]
    public class If : NodeBase
    {
        public If(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "If";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            if((bool)GetFieldOrLastInputField(2, data))
            {
                ActiveNextEvent(1, data);
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Event, "If", ConnectionType.EventOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Condition", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

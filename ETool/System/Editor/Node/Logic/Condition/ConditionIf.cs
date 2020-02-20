using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Condition/If")]
    [Logic_Menu("Condition")]
    public class ConditionIf : NodeBase
    {
        public ConditionIf(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "If";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            if ((bool)GetFieldOrLastInputField(1, data))
            {
                ActiveNextEvent(0, data);
            }
            ActiveNextEvent(2, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "If", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Condition", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Event, "Finish", ConnectionType.EventOutput, this, FieldContainer.Object));
        }
    }
}

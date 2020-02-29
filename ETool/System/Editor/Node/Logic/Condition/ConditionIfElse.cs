using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Condition/IfElse")]
    [Logic_Menu("Condition")]
    public class ConditionIfElse : NodeBase
    {
        public ConditionIfElse(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "If Else";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            if (GetFieldOrLastInputField<bool>(2, data))
            {
                ActiveNextEvent(0, data);
            }
            else
            {
                ActiveNextEvent(1, data);
            }
            ActiveNextEvent(3, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "If", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Event, "Else", ConnectionType.EventOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Condition", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Event, "Finish", ConnectionType.EventOutput, this, FieldContainer.Object));
        }
    }
}

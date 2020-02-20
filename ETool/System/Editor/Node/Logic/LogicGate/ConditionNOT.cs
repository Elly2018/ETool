using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Gate/NOT")]
    [Logic_Menu("Gate")]
    public class ConditionNOT : NodeBase
    {
        public ConditionNOT(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "NOT";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Condition", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 1)]
        public bool GetResult(BlueprintInput data)
        {
            bool a = (bool)GetFieldOrLastInputField(0, data);
            return !a;
        }
    }
}

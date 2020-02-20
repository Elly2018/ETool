using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Gate/NAND")]
    [Logic_Menu("Gate")]
    public class ConditionNAND : NodeBase
    {
        public ConditionNAND(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "NAND";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Condition A", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Condition B", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 2)]
        public bool GetResult(BlueprintInput data)
        {
            bool a = (bool)GetFieldOrLastInputField(0, data);
            bool b = (bool)GetFieldOrLastInputField(1, data);
            return !(a && b);
        }
    }
}

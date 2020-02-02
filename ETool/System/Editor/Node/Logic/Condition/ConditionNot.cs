using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Condition/Not")]
    public class ConditionNot : NodeBase
    {
        public ConditionNot(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Not";
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

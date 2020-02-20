using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Boolean/BooleanToInt")]
    [Casting_Menu("Boolean")]
    public class BooleanToInt : NodeBase
    {
        public BooleanToInt(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Boolean => Int";
        }

        [NodePropertyGet(typeof(int), 1)]
        public int GetString(BlueprintInput data)
        {
            return (bool)GetFieldOrLastInputField(0, data) ? 1 : 0;
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Boolean", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

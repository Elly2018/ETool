using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Boolean/BooleanToString")]
    [Casting_Menu("Boolean")]
    public class BooleanToString : NodeBase
    {
        public BooleanToString(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Boolean => String";
        }

        [NodePropertyGet(typeof(String), 1)]
        public string GetString(BlueprintInput data)
        {
            return ((Boolean)GetFieldOrLastInputField(0, data)).ToString();
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Boolean", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

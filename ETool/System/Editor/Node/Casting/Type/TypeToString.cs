using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Type/=>String")]
    public class TypeToString : NodeBase
    {
        public TypeToString(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Type => String";
        }

        [NodePropertyGet(typeof(String), 0)]
        public string GetString(BlueprintInput data)
        {
            return ((FieldType)GetFieldInputValue(0, data)).ToString();
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

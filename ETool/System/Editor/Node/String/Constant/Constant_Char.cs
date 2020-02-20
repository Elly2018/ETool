using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/String/Constant/Char")]
    [Constant_Menu]
    public class Constant_Char : NodeBase
    {
        public Constant_Char(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Char";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Char, "Char", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Char), 0)]
        public Char GetString(BlueprintInput data)
        {
            return GetFieldOrLastInputField<char>(0, data);
        }
    }
}

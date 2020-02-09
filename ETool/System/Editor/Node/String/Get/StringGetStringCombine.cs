using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/String/Get/StringCombine")]
    public class String_Add_String : NodeBase
    {
        public String_Add_String(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "String + String";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "First String", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Second String", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(String), 2)]
        public string GetString(BlueprintInput data)
        {
            string s0 = (string)GetFieldOrLastInputField(0, data);
            string s1 = (string)GetFieldOrLastInputField(1, data);
            return s0 + s1;
        }
    }
}

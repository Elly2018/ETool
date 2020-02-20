using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/String/Constant/String")]
    [Constant_Menu]
    public class Constant_String : NodeBase
    {
        public Constant_String(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant String";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "String", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetString(BlueprintInput data)
        {
            return GetFieldOrLastInputField<string>(0, data);
        }
    }
}

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/String/Constant/String")]
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

        [NodePropertyGet(typeof(String), 0)]
        public String GetString(BlueprintInput data)
        {
            return (String)Field.GetObjectByFieldType(FieldType.String, fields[0].target);
        }
    }
}

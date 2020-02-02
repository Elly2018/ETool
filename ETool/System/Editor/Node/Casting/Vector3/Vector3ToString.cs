using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Vector3/ => String")]
    public class Vector3ToString : NodeBase
    {
        public Vector3ToString(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector3 => String";
        }

        [NodePropertyGet(typeof(String), 1)]
        public String GetString(BlueprintInput data)
        {
            return ((Vector3)GetFieldOrLastInputField(0, data)).ToString();
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector3, "Vector3", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Constant/Int")]
    public class Constant_Int : NodeBase
    {
        public Constant_Int(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Int";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Int", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Int32), 0)]
        public Int32 GetFloat(BlueprintInput data)
        {
            return (Int32)Field.GetObjectByFieldType(FieldType.Int, fields[0].target);
        }
    }
}

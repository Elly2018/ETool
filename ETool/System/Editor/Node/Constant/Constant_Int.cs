using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Constant/Int")]
    public class Constant_Int : NodeBase
    {
        public Constant_Int(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Constant Int";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Int", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Int32), 0)]
        public int GetFloat(BlueprintInput data)
        {
            return fields[0].target.target_Int;
        }
    }
}

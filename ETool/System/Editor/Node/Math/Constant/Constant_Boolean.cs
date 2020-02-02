using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Constant/Boolean")]
    public class Constant_Boolean : NodeBase
    {
        public Constant_Boolean(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Boolean";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Boolean", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 0)]
        public Boolean GetFloat(BlueprintInput data)
        {
            return (Boolean)Field.GetObjectByFieldType(FieldType.Boolean, fields[0].target);
        }
    }
}

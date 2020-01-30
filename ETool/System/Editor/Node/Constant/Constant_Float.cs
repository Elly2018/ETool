using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Constant/Float")]
    public class Constant_Float : NodeBase
    {
        public Constant_Float(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Constant Float";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Float", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public Single GetFloat(BlueprintInput data)
        {
            return (Single)Field.GetObjectByFieldType(FieldType.Float, fields[0].target);
        }
    }
}

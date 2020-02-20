using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Constant/Float")]
    [Constant_Menu(1)]
    public class Constant_Float : NodeBase
    {
        public Constant_Float(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Float";
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

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Quaternion/Identity")]
    [Math_Menu("Quaternion")]
    public class QuaternionIdentity : NodeBase
    {
        public QuaternionIdentity(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Identity";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Quaternion, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Quaternion), 0)]
        public Quaternion GetGameObject(BlueprintInput data)
        {
            return Quaternion.identity;
        }
    }
}

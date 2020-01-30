using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Quaternion/Identity")]
    public class QuaternionIdentity : NodeBase
    {
        public QuaternionIdentity(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Identity";
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

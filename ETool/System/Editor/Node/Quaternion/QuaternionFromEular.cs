using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Quaternion/From Eular")]
    public class QuaternionFromEular : NodeBase
    {
        public QuaternionFromEular(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Quaternion From Eular";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Quaternion, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Eular", ConnectionType.DataInput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Quaternion), 0)]
        public Quaternion GetGameObject(BlueprintInput data)
        {
            return Quaternion.Euler(((Vector3)GetFieldOrLastInputField(1, data)));
        }
    }
}

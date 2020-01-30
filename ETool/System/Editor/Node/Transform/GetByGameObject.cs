using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Transform/Get By GameObject")]
    public class GetByGameObject : NodeBase
    {
        public GetByGameObject(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get By GameObject";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameObject, "Find", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Transform), 1)]
        public Transform GetGameObject(BlueprintInput data)
        {
            return ((GameObject)GetFieldOrLastInputField(0, data)).transform;
        }
    }
}

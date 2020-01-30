using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Get By Transform")]
    public class GetByTransform : NodeBase
    {
        public GetByTransform(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get By Transform";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Transform, "Find", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 1)]
        public GameObject GetGameObject(BlueprintInput data)
        {
            return ((Transform)GetFieldOrLastInputField(0, data)).gameObject;
        }
    }
}


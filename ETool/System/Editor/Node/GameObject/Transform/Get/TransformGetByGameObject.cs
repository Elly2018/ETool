using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Transform/Get/GetTransformByGameObject")]
    [Transform_Menu("TransformFind")]
    public class TransformGetByGameObject : NodeBase
    {
        public TransformGetByGameObject(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Transform By GameObject";
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

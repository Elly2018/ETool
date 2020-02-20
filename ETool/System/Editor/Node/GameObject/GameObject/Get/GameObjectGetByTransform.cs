using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Get/GetByTransform")]
    [Transform_Menu("GameObjectFind")]
    public class GameObjectGetByTransform : NodeBase
    {
        public GameObjectGetByTransform(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get GameObject By Transform";
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


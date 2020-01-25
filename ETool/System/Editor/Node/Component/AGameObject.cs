using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Component/GameObject")]
    public class AGameObject : NodeBase
    {
        public AGameObject(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "GameObject";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 0)]
        public GameObject GetGameObject(BlueprintInput data)
        {
            return fields[0].target.target_GameObject;
        }
    }
}

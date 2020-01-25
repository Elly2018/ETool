using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Self GameObject")]
    public class SelfGameObject : NodeBase
    {
        public SelfGameObject(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Self GameObject";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 0)]
        public GameObject GetGameObject(BlueprintInput data)
        {
            return data.thisGameobject;
        }
    }
}


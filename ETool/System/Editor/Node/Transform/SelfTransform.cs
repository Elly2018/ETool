using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Transform/Self Transform")]
    public class SelfTransform : NodeBase
    {
        public SelfTransform(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Self Transform";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 0)]
        public Transform GetGameObject(BlueprintInput data)
        {
            return data.thisGameobject.transform;
        }
    }
}


using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Transform/Get/SelfTransform")]
    [Self_Menu]
    public class TransformSelfTransform : NodeBase
    {
        public TransformSelfTransform(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Self Transform";
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


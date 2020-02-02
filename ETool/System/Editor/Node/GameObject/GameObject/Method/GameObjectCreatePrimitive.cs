using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Method/CreatePrimitive")]
    public class CreatePrimitive : NodeBase
    {
        public CreatePrimitive(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Create Primitive";
        }
    }
}

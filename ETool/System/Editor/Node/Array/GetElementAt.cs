using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Get Element At")]
    public class GetElementAt : NodeBase
    {
        public GetElementAt(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Element At";
        }
    }
}


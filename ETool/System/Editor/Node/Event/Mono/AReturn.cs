using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Event/Return")]
    public class AReturn : NodeBase
    {
        public AReturn(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Return";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            base.ProcessCalling(data);
        }
    }
}

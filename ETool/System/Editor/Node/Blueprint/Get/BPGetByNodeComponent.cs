using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Blueprint/Get/GetByNodeComponent")]
    public class BPGetByNodeComponent : NodeBase
    {
        public BPGetByNodeComponent(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get BP By NodeComponent";
        }

        public override void FieldInitialize()
        {
            base.FieldInitialize();
        }
    }
}

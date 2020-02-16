using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Event/OnDestory")]
    [NodeHide]
    [CanNotCopy]
    [CanNotDelete]
    public class AOnDestory : NodeBase
    {
        public AOnDestory(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "On Destroy";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventOutput, this, FieldContainer.Object));
        }

        public override StyleType GetNodeStyle()
        {
            return StyleType.Event_Node;
        }

        public override StyleType GetNodeSelectStyle()
        {
            return StyleType.Select_Event_Node;
        }
    }
}

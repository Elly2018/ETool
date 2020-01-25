using UnityEngine;

namespace ETool.ANode
{
    [System.Serializable]
    public class AFixedUpdate : NodeBase
    {
        public AFixedUpdate(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Fixed Update";
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

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Event/OnTriggerStay")]
    [NodeHide]
    public class AOnTriggerStay : NodeBase
    {
        private Collider collision;

        public AOnTriggerStay(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "OnTriggerStay";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            collision = data.m_Collider;
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Collision, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        public override StyleType GetNodeStyle()
        {
            return StyleType.Event_Node;
        }

        public override StyleType GetNodeSelectStyle()
        {
            return StyleType.Select_Event_Node;
        }

        [NodePropertyGet(typeof(Collider), 1)]
        public Collider GetCollision(BlueprintInput data)
        {
            return collision;
        }
    }
}

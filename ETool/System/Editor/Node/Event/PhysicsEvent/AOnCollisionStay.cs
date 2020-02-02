using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Event/OnCollisionStay")]
    [NodeHide]
    public class AOnCollisionStay : NodeBase
    {
        private Collision collision;

        public AOnCollisionStay(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "OnCollisionStay";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            collision = data.m_Collision;
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

        [NodePropertyGet(typeof(Collision), 1)]
        public Collision GetCollision(BlueprintInput data)
        {
            return collision;
        }
    }
}

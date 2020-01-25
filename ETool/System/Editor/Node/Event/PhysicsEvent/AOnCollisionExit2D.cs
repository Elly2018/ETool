using UnityEngine;

namespace ETool.ANode
{
    [System.Serializable]
    public class AOnCollisionExit2D : NodeBase
    {
        private Collision2D collision;

        public AOnCollisionExit2D(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "OnCollisionExit2D";
            description = "";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            collision = data.m_Collision2D;
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Collision2D, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        public override StyleType GetNodeStyle()
        {
            return StyleType.Event_Node;
        }

        public override StyleType GetNodeSelectStyle()
        {
            return StyleType.Select_Event_Node;
        }

        [NodePropertyGet(typeof(Collision2D), 1)]
        public Collision2D GetCollision(BlueprintInput data)
        {
            return collision;
        }
    }
}

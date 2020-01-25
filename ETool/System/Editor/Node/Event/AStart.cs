using UnityEngine;

namespace ETool.ANode
{
    [System.Serializable]
    public class AStart : NodeBase
    {
        public AStart(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Start";
            description =
                "Start is called on the frame when a script is enabled \n" +
                "Just before any of the Update methods are called the first time \n";
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

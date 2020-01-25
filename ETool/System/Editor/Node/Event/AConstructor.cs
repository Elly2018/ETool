using UnityEngine;

namespace ETool.ANode
{
    [System.Serializable]
    public class AConstructor : NodeBase
    {
        public AConstructor(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Constructor";
            description = 
                "A constructor is a special method that is used to initialize Blueprint \n" +
                "The advantage of a constructor is that it is called when an object of a class is created \n" +
                "It can be used to set initial values for fields";
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


using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Application/Method/Quit")]
    public class Quit : NodeBase
    {
        public Quit(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Quit";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Application.Quit();
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
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

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Event/Editor Stop")]
    public class EditorStop : NodeBase
    {
        public EditorStop(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Editor Stop";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            if(Application.isEditor)
                UnityEditor.EditorApplication.isPlaying = false;
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

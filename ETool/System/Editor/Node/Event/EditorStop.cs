﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Event/EditorStop")]
    public class EditorStop : NodeBase
    {
        public EditorStop(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Editor Stop";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
#if UNITY_EDITOR
            if(Application.isEditor)
                UnityEditor.EditorApplication.isPlaying = false;
#endif
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

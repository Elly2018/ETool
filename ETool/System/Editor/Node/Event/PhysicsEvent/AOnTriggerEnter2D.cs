﻿using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Event/OnTriggerEnter2D")]
    [NodeHide]
    [CanNotCopy]
    public class AOnTriggerEnter2D : NodeBase
    {
        private Collider2D collision;

        public AOnTriggerEnter2D(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "OnTriggerEnter2D";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            collision = data.m_Collider2D;
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

        [NodePropertyGet(typeof(Collider2D), 1)]
        public Collider2D GetCollision(BlueprintInput data)
        {
            return collision;
        }
    }
}

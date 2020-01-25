﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    public class AUpdate : NodeBase
    {
        public AUpdate(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Update";
            description = 
                "Update is called every frame \n" +
                "Update is the most commonly used function to implement any kind of game script \n" +
                "Not every Blueprint needs Update";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Delta", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        public override StyleType GetNodeStyle()
        {
            return StyleType.Event_Node;
        }

        public override StyleType GetNodeSelectStyle()
        {
            return StyleType.Select_Event_Node;
        }

        [NodePropertyGet(typeof(Single), 1)]
        public float GetFloat(BlueprintInput data)
        {
            return Time.deltaTime;
        }
    }
}

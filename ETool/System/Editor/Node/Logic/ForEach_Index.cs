﻿using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/ForEach Index")]
    public class ForEach_Index : NodeBase
    {
        private int index;

        public ForEach_Index(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "For Each";
            index = -1;
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            int count = (int)GetFieldOrLastInputField(2, data);

            for(int i = 0; i < count; i++)
            {
                index = i;
                ActiveNextEvent(0, data);
            }

            index = -1;
            ActiveNextEvent(3, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Index", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Count", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Event, "Finish", ConnectionType.EventOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 1)]
        public int GetIndex(BlueprintInput data)
        {
            return index;
        }
    }
}
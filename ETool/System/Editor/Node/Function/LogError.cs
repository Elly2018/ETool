using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Utility/Log Error")]
    public class LogError : NodeBase
    {
        public LogError(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Log Error";
            description = "Print a message string in unity console";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Debug.LogError((string)GetFieldOrLastInputField(1, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Message", ConnectionType.DataInput, this, FieldContainer.Object));

            fields[1].target.target_String = "New String";
        }
    }
}

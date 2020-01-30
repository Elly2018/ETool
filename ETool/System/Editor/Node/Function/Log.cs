using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Utility/Log")]
    public class Log : NodeBase
    {
        public Log(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Log";
            description = "Print a message string in unity console";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Debug.Log((string)GetFieldOrLastInputField(1, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Message", ConnectionType.DataInput, this, FieldContainer.Object));
            Field.SetObjectByFieldType(FieldType.String, fields[1].target, "New String");
        }
    }
}

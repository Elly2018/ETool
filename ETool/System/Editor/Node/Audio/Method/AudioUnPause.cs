using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Audio/Method/UnPause")]
    public class AudioUnPause : NodeBase
    {
        public AudioUnPause(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "UnPause";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            AudioSource ass = GetFieldOrLastInputField<AudioSource>(1, data);
            if (ass != null)
                ass.UnPause();
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Node.Field.Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AudioSource, "Node.Field.Source", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}



using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Audio/Method/Stop")]
    public class AudioStop : NodeBase
    {
        public AudioStop(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Stop";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            AudioSource ass = GetFieldOrLastInputField<AudioSource>(1, data);
            if (ass != null)
                ass.Stop();
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AudioSource, "Source", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

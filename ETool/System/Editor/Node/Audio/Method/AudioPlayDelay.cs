using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Audio/Method/PlayDelay")]
    public class AudioPlayDelay : NodeBase
    {
        public AudioPlayDelay(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Play Delay";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            AudioSource ass = GetFieldOrLastInputField<AudioSource>(1, data);
            float delay = GetFieldOrLastInputField<float>(2, data);
            if (ass != null)
                ass.PlayDelayed(delay);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AudioSource, "Source", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Delay", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}


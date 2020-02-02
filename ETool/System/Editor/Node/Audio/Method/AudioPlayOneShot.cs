using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Audio/Method/PlayOneShot")]
    public class AudioPlayOneShot : NodeBase
    {
        public AudioPlayOneShot(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Play OneShot";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            AudioSource ass = GetFieldOrLastInputField<AudioSource>(1, data);
            AudioClip clip = GetFieldOrLastInputField<AudioClip>(2, data);
            float vol = GetFieldOrLastInputField<float>(3, data);
            if (ass != null)
                ass.PlayOneShot(clip, vol);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AudioSource, "Source", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AudioClip, "Clip", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Volume", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}



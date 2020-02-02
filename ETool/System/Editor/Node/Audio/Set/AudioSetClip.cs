using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Audio/Set/SetClip")]
    public class AudioSetClip : NodeBase
    {
        public AudioSetClip(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Set Audio Clip";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            AudioSource ass = GetFieldOrLastInputField<AudioSource>(1, data);
            AudioClip asc = GetFieldOrLastInputField<AudioClip>(2, data);
            if (ass != null && asc != null)
                ass.clip = asc;

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AudioSource, "Source", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AudioClip, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(AudioSource), 1)]
        public AudioSource GetSource(BlueprintInput data)
        {
            AudioSource ass = GetFieldOrLastInputField<AudioSource>(1, data);
            if (ass != null)
                return ass;

            return null;
        }

        [NodePropertyGet(typeof(AudioClip), 2)]
        public AudioClip GetClip(BlueprintInput data)
        {
            AudioSource ass = GetFieldOrLastInputField<AudioSource>(1, data);
            if (ass != null)
                return ass.clip;

            return null;
        }
    }
}

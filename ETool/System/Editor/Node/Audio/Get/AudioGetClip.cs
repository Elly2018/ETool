using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Audio/Get/GetClip")]
    public class AudioGetClip : NodeBase
    {
        public AudioGetClip(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Audio Clip";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.AudioSource, "Source", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AudioClip, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(AudioClip), 1)]
        public AudioClip GetClip(BlueprintInput data)
        {
            AudioSource ass = GetFieldOrLastInputField<AudioSource>(0, data);
            if (ass != null)
                return ass.clip;

            return null;
        }
    }
}

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Audio/Constant/AudioMixer")]
    [Constant_Menu]
    public class Constant_AudioMixer : NodeBase
    {
        public Constant_AudioMixer(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Audio Mixer";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.AudioMixer, "Mixer", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(AudioClip), 0)]
        public AudioClip GetFloat(BlueprintInput data)
        {
            return (AudioClip)Field.GetObjectByFieldType(FieldType.AudioClip, fields[0].target);
        }
    }
}



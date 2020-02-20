using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Audio/Constant/AudioClip")]
    [Constant_Menu]
    public class Constant_AudioClip : NodeBase
    {
        public Constant_AudioClip(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Audio Clip";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.AudioClip, "Clip", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(AudioClip), 0)]
        public AudioClip GetFloat(BlueprintInput data)
        {
            return (AudioClip)Field.GetObjectByFieldType(FieldType.AudioClip, fields[0].target);
        }
    }
}


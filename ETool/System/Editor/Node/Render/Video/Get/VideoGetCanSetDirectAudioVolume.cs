﻿using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetCanSetDirectAudioVolume")]
    public class VideoGetCanSetDirectAudioVolume : NodeBase
    {
        public VideoGetCanSetDirectAudioVolume(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Can Set Direct Audio Volume";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).canSetDirectAudioVolume;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

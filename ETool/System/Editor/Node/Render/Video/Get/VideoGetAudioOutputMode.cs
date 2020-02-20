using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetAudioOutputMode")]
    public class VideoGetAudioOutputMode : NodeBase
    {
        public VideoGetAudioOutputMode(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Audio OutputMode";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.VideoAudioOutputMode, "Output Mode", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoAudioOutputMode), 0)]
        public VideoAudioOutputMode GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).audioOutputMode;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

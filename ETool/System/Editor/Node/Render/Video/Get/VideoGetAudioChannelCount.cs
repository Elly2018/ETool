using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetAudioChannelCount")]
    public class VideoGetAudioChannelCount : NodeBase
    {
        public VideoGetAudioChannelCount(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Audio Channel Count";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Channel Count", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "TrackIndex", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data).GetAudioChannelCount((ushort)GetFieldOrLastInputField<int>(1, data));
        }

        [NodePropertyGet(typeof(VideoPlayer), 2)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data);
        }
    }
}

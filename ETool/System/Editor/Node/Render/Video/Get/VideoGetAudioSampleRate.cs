using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetAudioSampleRate")]
    public class VideoGetAudioSampleRate : NodeBase
    {
        public VideoGetAudioSampleRate(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Audio Sample Rate";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Audio Sample Rate", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "TrackIndex", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetID0(BlueprintInput data)
        {
            return (int)GetFieldOrLastInputField<VideoPlayer>(2, data).GetAudioSampleRate((ushort)GetFieldOrLastInputField<int>(1, data));
        }

        [NodePropertyGet(typeof(VideoPlayer), 2)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data);
        }
    }
}

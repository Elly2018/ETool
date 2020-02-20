using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetTargetAudioSource")]
    public class VideoGetTargetAudioSource : NodeBase
    {
        public VideoGetTargetAudioSource(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Target AudioSource";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.AudioSource, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "TrackIndex", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(AudioSource), 0)]
        public AudioSource GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data).GetTargetAudioSource((ushort)GetFieldOrLastInputField<int>(1, data));
        }

        [NodePropertyGet(typeof(VideoPlayer), 2)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data);
        }
    }
}

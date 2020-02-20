using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetDirectAudioVolume")]
    public class VideoGetDirectAudioVolume : NodeBase
    {
        public VideoGetDirectAudioVolume(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Direct Audio Volume";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Volume", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "TrackIndex", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data).GetDirectAudioVolume((ushort)GetFieldOrLastInputField<int>(1, data));
        }

        [NodePropertyGet(typeof(VideoPlayer), 2)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data);
        }
    }
}

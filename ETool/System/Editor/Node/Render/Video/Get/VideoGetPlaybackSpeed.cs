using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetPlaybackSpeed")]
    public class VideoGetPlaybackSpeed : NodeBase
    {
        public VideoGetPlaybackSpeed(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Playback Speed";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Playback Speed", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data).playbackSpeed;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

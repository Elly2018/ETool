using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetAudioTrackCount")]
    public class VideoGetAudioTrackCount : NodeBase
    {
        public VideoGetAudioTrackCount(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Audio Track Count";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Track Count", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).audioTrackCount;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

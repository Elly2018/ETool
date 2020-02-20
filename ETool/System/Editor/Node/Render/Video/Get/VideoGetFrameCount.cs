using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetFrameCount")]
    public class VideoGetFrameCount : NodeBase
    {
        public VideoGetFrameCount(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Frame Count";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Long, "Frame Count", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(long), 0)]
        public long GetID0(BlueprintInput data)
        {
            return (long)GetFieldOrLastInputField<VideoPlayer>(2, data).frameCount;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

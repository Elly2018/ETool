using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetFrame")]
    public class VideoGetFrame : NodeBase
    {
        public VideoGetFrame(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Frame";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Long, "Frame", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(long), 0)]
        public long GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data).frame;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

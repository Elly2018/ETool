using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetSource")]
    public class VideoGetSource : NodeBase
    {
        public VideoGetSource(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Source";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.VideoSource, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoSource), 0)]
        public VideoSource GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).source;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetAspectRatio")]
    public class VideoGetAspectRatio : NodeBase
    {
        public VideoGetAspectRatio(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Aspect Ratio";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.VideoAspectRatio, "Aspect Ratio", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoAspectRatio), 0)]
        public VideoAspectRatio GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).aspectRatio;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

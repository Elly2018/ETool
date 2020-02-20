using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetRenderMode")]
    public class VideoGetRenderMode : NodeBase
    {
        public VideoGetRenderMode(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Can Render Mode";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.VideoRenderMode, "Mode", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoRenderMode), 0)]
        public VideoRenderMode GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).renderMode;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

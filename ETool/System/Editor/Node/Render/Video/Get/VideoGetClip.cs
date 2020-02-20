using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetClip")]
    public class VideoGetClip : NodeBase
    {
        public VideoGetClip(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Clip";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Clip, "Clip", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoClip), 0)]
        public VideoClip GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).clip;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

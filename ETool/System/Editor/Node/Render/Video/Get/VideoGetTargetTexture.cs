using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetTargetTexture")]
    public class VideoGetTargetTexture : NodeBase
    {
        public VideoGetTargetTexture(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Target Texture";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.RenderTexture, "Texture", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(RenderTexture), 0)]
        public RenderTexture GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).targetTexture;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

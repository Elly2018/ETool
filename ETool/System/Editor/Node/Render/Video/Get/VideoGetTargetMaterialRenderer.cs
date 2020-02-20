using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetTargetMaterialRenderer")]
    public class VideoGetTargetMaterialRenderer : NodeBase
    {
        public VideoGetTargetMaterialRenderer(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Target Material Renderer";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Renderer, "Renderer", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Renderer), 0)]
        public Renderer GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).targetMaterialRenderer;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

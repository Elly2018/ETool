using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetTargetMaterialRenderer")]
    public class VideoSetTargetMaterialRenderer : NodeBase
    {
        public VideoSetTargetMaterialRenderer(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Target Material Renderer";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).targetMaterialRenderer = GetFieldOrLastInputField<Renderer>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Renderer, "Renderer", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(Renderer), 2)]
        public Renderer GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Renderer>(2, data);
        }
    }
}


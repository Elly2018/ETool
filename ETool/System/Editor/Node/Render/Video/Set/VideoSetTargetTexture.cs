using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetTargetTexture")]
    public class VideoSetTargetTexture : NodeBase
    {
        public VideoSetTargetTexture(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Target Texture";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).targetTexture = GetFieldOrLastInputField<RenderTexture>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.RenderTexture, "Texture", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(RenderTexture), 2)]
        public RenderTexture GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<RenderTexture>(2, data);
        }
    }
}


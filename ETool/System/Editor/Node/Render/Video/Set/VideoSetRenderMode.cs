using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetRenderMode")]
    public class VideoSetRenderMode : NodeBase
    {
        public VideoSetRenderMode(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Render Mode";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).renderMode = GetFieldOrLastInputField<VideoRenderMode>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoRenderMode, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(VideoRenderMode), 2)]
        public VideoRenderMode GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoRenderMode>(2, data);
        }
    }
}


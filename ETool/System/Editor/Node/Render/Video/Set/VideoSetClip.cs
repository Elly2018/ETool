using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetClip")]
    public class VideoSetClip : NodeBase
    {
        public VideoSetClip(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Clip";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).clip = GetFieldOrLastInputField<VideoClip>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Clip, "Clip", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(VideoClip), 2)]
        public VideoClip GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoClip>(2, data);
        }
    }
}


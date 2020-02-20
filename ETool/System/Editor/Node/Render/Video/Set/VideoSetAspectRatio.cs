using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetAspectRatio")]
    public class VideoSetAspectRatio : NodeBase
    {
        public VideoSetAspectRatio(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set AspectRatio";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).aspectRatio = GetFieldOrLastInputField<VideoAspectRatio>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoAspectRatio, "Aspect Ratio", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(VideoAspectRatio), 2)]
        public VideoAspectRatio GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoAspectRatio>(2, data);
        }
    }
}


using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetFrame")]
    public class VideoSetFrame : NodeBase
    {
        public VideoSetFrame(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Frame";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).frame = GetFieldOrLastInputField<long>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Long, "Frame", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(long), 2)]
        public long GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<long>(2, data);
        }
    }
}


using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetTime")]
    public class VideoSetTime : NodeBase
    {
        public VideoSetTime(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Time";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).time = GetFieldOrLastInputField<double>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Double, "Time", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(string), 2)]
        public double GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<double>(2, data);
        }
    }
}


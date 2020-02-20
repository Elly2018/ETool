using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetWaitForFirstFrame")]
    public class VideoSetWaitForFirstFrame : NodeBase
    {
        public VideoSetWaitForFirstFrame(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Wait For First Frame";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).waitForFirstFrame = GetFieldOrLastInputField<bool>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Value", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(bool), 2)]
        public bool GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<bool>(2, data);
        }
    }
}


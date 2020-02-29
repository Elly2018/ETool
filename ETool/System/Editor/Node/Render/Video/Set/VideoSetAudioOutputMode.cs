using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetAudioOutputMode")]
    public class VideoSetAudioOutputMode : NodeBase
    {
        public VideoSetAudioOutputMode(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Audio Output Mode";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).audioOutputMode = GetFieldOrLastInputField<VideoAudioOutputMode>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoAudioOutputMode, "Output Mode", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(VideoAspectRatio), 2)]
        public VideoAudioOutputMode GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoAudioOutputMode>(2, data);
        }
    }
}


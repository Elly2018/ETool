using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetTargetCameraAlpha")]
    public class VideoSetTargetCameraAlpha : NodeBase
    {
        public VideoSetTargetCameraAlpha(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Target Camera Alpha";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).targetCameraAlpha = GetFieldOrLastInputField<float>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Alpha", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(float), 2)]
        public float GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<float>(2, data);
        }
    }
}


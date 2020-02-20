using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetDirectAudioVolume")]
    public class VideoSetDirectAudioVolume : NodeBase
    {
        public VideoSetDirectAudioVolume(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Direct Audio Volume";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).SetDirectAudioVolume((ushort)GetFieldOrLastInputField<int>(2, data), GetFieldOrLastInputField<float>(3, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "ChannelIndex", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Volumn", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}


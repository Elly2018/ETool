using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetTargetAudioSource")]
    public class VideoSetTargetAudioSource : NodeBase
    {
        public VideoSetTargetAudioSource(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Target AudioSource";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).SetTargetAudioSource((ushort)GetFieldOrLastInputField<int>(2, data), GetFieldOrLastInputField<AudioSource>(3, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "ChannelIndex", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AudioSource, "Source", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(AudioSource), 3)]
        public AudioSource GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<AudioSource>(3, data);
        }
    }
}


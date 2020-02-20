using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/IsAudioTrackEnabled")]
    public class VideoIsAudioTrackEnabled : NodeBase
    {
        public VideoIsAudioTrackEnabled(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Is Audio Track Enabled";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Is Enable", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "TrackIndex", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data).IsAudioTrackEnabled((ushort)GetFieldOrLastInputField<int>(1, data));
        }

        [NodePropertyGet(typeof(VideoPlayer), 2)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(2, data);
        }
    }
}


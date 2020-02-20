using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetTime")]
    public class VideoGetTime : NodeBase
    {
        public VideoGetTime(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Time";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Double, "Time", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(double), 0)]
        public double GetID0(BlueprintInput data)
        {
            return (int)GetFieldOrLastInputField<VideoPlayer>(2, data).time;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

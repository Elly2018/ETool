using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetClockTime")]
    public class VideoGetClockTime : NodeBase
    {
        public VideoGetClockTime(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Clock Time";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Double, "Clock Time", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(double), 0)]
        public double GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).clockTime;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

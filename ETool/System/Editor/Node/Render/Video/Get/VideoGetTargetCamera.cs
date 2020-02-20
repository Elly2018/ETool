using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetTargetCamera")]
    public class VideoGetTargetCamera : NodeBase
    {
        public VideoGetTargetCamera(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Target Camera";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Camera, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Camera), 0)]
        public Camera GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).targetCamera;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

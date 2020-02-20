using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetHeight")]
    public class VideoGetHeight : NodeBase
    {
        public VideoGetHeight(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Height";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Height", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetID0(BlueprintInput data)
        {
            return (int)GetFieldOrLastInputField<VideoPlayer>(2, data).height;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

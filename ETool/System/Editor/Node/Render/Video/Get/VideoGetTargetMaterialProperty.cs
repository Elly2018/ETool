using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Get/GetTargetMaterialProperty")]
    public class VideoGetTargetMaterialProperty : NodeBase
    {
        public VideoGetTargetMaterialProperty(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Target Material Property";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Property", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetID0(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data).targetMaterialProperty;
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }
    }
}

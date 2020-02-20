using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Video/Set/SetTargetMaterialProperty")]
    public class VideoSetTargetMaterialProperty : NodeBase
    {
        public VideoSetTargetMaterialProperty(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Target Material Property";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<VideoPlayer>(1, data).targetMaterialProperty = GetFieldOrLastInputField<string>(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.VideoPlayer, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Property", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(VideoPlayer), 1)]
        public VideoPlayer GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<VideoPlayer>(1, data);
        }

        [NodePropertyGet(typeof(string), 2)]
        public string GetValue(BlueprintInput data)
        {
            return GetFieldOrLastInputField<string>(2, data);
        }
    }
}


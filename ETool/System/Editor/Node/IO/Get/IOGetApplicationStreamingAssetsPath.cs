using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Get/GetApplicationStreamingAssetsPath")]
    public class IOGetApplicationStreamingAssetsPath : NodeBase
    {
        public IOGetApplicationStreamingAssetsPath(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Application Streaming Assets Path";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetTouches(BlueprintInput data)
        {
            return Application.streamingAssetsPath;
        }
    }
}



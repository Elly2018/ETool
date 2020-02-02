using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Get/GetApplicationTemporaryCachePath")]
    public class IOGetApplicationTemporaryCachePath : NodeBase
    {
        public IOGetApplicationTemporaryCachePath(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Application Temporary Cache Path";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetTouches(BlueprintInput data)
        {
            return Application.temporaryCachePath;
        }
    }
}



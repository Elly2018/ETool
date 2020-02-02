using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Get/GetApplicationPersistentDataPath")]
    public class IOGetApplicationPersistentDataPath : NodeBase
    {
        public IOGetApplicationPersistentDataPath(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Application Persistent Datapath";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetTouches(BlueprintInput data)
        {
            return Application.persistentDataPath;
        }
    }
}



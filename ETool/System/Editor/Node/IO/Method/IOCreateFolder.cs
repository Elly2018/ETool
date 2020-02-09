using System.IO;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Method/Create Folder")]
    public class IOCreateFolder : NodeBase
    {
        public IOCreateFolder(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Create Folder";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Directory.CreateDirectory(GetFieldOrLastInputField<string>(1, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 2)]
        public string GetResultPath(BlueprintInput data)
        {
            return GetFieldOrLastInputField<string>(2, data);
        }
    }
}
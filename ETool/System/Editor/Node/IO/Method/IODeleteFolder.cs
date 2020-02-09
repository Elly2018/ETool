using System.IO;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Method/Delete Folder")]
    public class IODeleteFolder : NodeBase
    {
        public IODeleteFolder(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Delete Folder";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Directory.Delete(GetFieldOrLastInputField<string>(1, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}
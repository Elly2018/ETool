using System.IO;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Method/Copy File")]
    public class IOCopyFile : NodeBase
    {
        public IOCopyFile(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Copy File";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            File.Copy(GetFieldOrLastInputField<string>(1, data), GetFieldOrLastInputField<string>(2, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 2)]
        public string GetResultPath(BlueprintInput data)
        {
            return GetFieldOrLastInputField<string>(2, data);
        }
    }
}
using System.IO;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Get/GetFolderFolders")]
    public class IOGetFolderFolders : NodeBase
    {
        public IOGetFolderFolders(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Folder Folders";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(string[]), 1)]
        public string[] GetTouches(BlueprintInput data)
        {
            return Directory.GetDirectories(GetFieldOrLastInputField<string>(0, data));
        }
    }
}



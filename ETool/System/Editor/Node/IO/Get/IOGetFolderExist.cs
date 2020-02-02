using System.IO;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Get/GetFolderExist")]
    public class IOGetFolderExist : NodeBase
    {
        public IOGetFolderExist(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Folder Exist";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 1)]
        public bool GetTouches(BlueprintInput data)
        {
            return Directory.Exists(GetFieldOrLastInputField<string>(0, data));
        }
    }
}



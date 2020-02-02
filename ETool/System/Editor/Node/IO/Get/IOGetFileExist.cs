using System.IO;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Get/GetFileExist")]
    public class IOGetFileExist : NodeBase
    {
        public IOGetFileExist(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get File Exist";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 1)]
        public bool GetTouches(BlueprintInput data)
        {
            return File.Exists(GetFieldOrLastInputField<string>(0, data));
        }
    }
}



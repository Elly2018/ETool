using System.IO;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Method/Copy Folder")]
    public class IOCopyFolder : NodeBase
    {
        public IOCopyFolder(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Copy Folder";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            string source = GetFieldOrLastInputField<string>(1, data);
            string des = GetFieldOrLastInputField<string>(2, data);
            bool overwrite = GetFieldOrLastInputField<bool>(3, data);

            foreach(string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(source, des));
            }

            foreach(string filePath in Directory.GetFiles(source, ".", SearchOption.AllDirectories))
            {
                File.Copy(filePath, filePath.Replace(source, des), overwrite);
            }

            ActiveNextEvent(0, data);
        }


        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Overwrite", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 2)]
        public string GetResultPath(BlueprintInput data)
        {
            return GetFieldOrLastInputField<string>(2, data);
        }
    }
}
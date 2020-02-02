using System.IO;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Get/ReadFileAllText")]
    public class IOReadAllText : NodeBase
    {
        public IOReadAllText(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Read File All Text";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 1)]
        public string GetTouches(BlueprintInput data)
        {
            return File.ReadAllText(GetFieldOrLastInputField<string>(0, data));
        }
    }
}
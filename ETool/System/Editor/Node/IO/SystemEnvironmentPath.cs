using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/System Environment Path")]
    public class SystemEnvironmentPath : NodeBase
    {
        public SystemEnvironmentPath(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Environment Path";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.EnvironemntPath, "Environment", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 1)]
        public string GetResultPath(BlueprintInput data)
        {
            return Environment.GetFolderPath(GetFieldOrLastInputField<Environment.SpecialFolder>(0, data));
        }
    }
}



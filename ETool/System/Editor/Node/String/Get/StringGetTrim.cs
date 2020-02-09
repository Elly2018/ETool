using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/String/Get/Trim")]
    public class StringGetTrim : NodeBase
    {
        public StringGetTrim(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "String Trim";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Char, "Value", ConnectionType.DataInput, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetResult(BlueprintInput data)
        {
            string v1 = GetFieldOrLastInputField<string>(1, data);
            char[] v2 = GetFieldOrLastInputField<char[]>(2, data);
            return v1.Trim(v2);
        }
    }
}

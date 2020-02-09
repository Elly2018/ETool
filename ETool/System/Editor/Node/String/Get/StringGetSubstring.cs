using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/String/Get/SubString")]
    public class StringGetSubstring : NodeBase
    {
        public StringGetSubstring(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "String SubString";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.String, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "StartIndex", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Length", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetResult(BlueprintInput data)
        {
            string v1 = GetFieldOrLastInputField<string>(1, data);
            int v2 = GetFieldOrLastInputField<int>(2, data);
            int v3 = GetFieldOrLastInputField<int>(3, data);
            return v1.Substring(v2, v3);
        }
    }
}

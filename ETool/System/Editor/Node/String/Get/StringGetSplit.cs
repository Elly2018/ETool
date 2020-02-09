using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/String/Get/Split")]
    public class StringGetSplit : NodeBase
    {
        public StringGetSplit(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "String Split";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.String, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Char, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string[]), 0)]
        public string[] GetResult(BlueprintInput data)
        {
            string v1 = GetFieldOrLastInputField<string>(1, data);
            char v2 = GetFieldOrLastInputField<char>(2, data);
            return v1.Split(v2);
        }
    }
}

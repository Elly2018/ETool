using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Char/CharArrayToString")]
    public class CharArrayToString : NodeBase
    {
        public CharArrayToString(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Char Array To String";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Char, "Char Array", ConnectionType.DataInput, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 1)]
        public string GetString(BlueprintInput data)
        {
            char[] array = GetFieldOrLastInputField<char[]>(0, data);
            return array.ToString();
        }
    }
}

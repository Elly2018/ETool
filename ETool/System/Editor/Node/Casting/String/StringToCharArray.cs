using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/String/StringToCharArray")]
    public class StringToCharArray : NodeBase
    {
        public StringToCharArray(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "String To Char Array";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "String", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(string[]), 1)]
        public string[] GetString(BlueprintInput data)
        {
            string o = GetFieldOrLastInputField<string>(0, data);
            char[] t = o.ToCharArray();
            string[] result = new string[t.Length];
            for(int i = 0; i < result.Length; i++)
            {
                result[i] = t[i].ToString();
            }

            return result;
        }
    }
}


using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/String/Get/Replace")]
    public class StringGetReplace : NodeBase
    {
        public StringGetReplace(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "String Replace";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Find", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Replace", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 1)]
        public string GetResult(BlueprintInput data)
        {
            string v1 = GetFieldOrLastInputField<string>(0, data);
            string v2 = GetFieldOrLastInputField<string>(1, data);
            string v3 = GetFieldOrLastInputField<string>(2, data);
            return v1.Replace(v2, v3);
        }
    }
}

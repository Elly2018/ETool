using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/String/Method/Find")]
    public class StringFindString : NodeBase
    {
        public StringFindString(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "String Find";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Find", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetResult(BlueprintInput data)
        {
            string v1 = GetFieldOrLastInputField<string>(1, data);
            string v2 = GetFieldOrLastInputField<string>(2, data);
            return v1.Contains(v2);
        }
    }
}

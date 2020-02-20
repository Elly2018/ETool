using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/String/Get/StringCombine")]
    public class String_Add_String : NodeBase
    {
        public String_Add_String(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "String + String";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "String Count", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "String 0", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            Update();
        }

        public override void FieldUpdate()
        {
            Update();
        }

        private void Update()
        {
            bool change = true;
            while (change)
            {
                change = false;
                if (fields.Count - 3 > (int)fields[1].GetValue(FieldType.Int))
                {
                    change = true;
                    DeleteLastField();
                }
                if (fields.Count - 3 < (int)fields[1].GetValue(FieldType.Int))
                {
                    change = true;
                    fields.Add(new Field(FieldType.String, "String " + (fields.Count - 2).ToString(), ConnectionType.DataInput, this, FieldContainer.Object));
                }
            }
        }

        [NodePropertyGet(typeof(object), 0)]
        public object GetAnswer(BlueprintInput data)
        {
            string answer = (string)GetFieldOrLastInputField(2, data);
            for (int i = 3; i < fields.Count; i++)
            {
                answer += (string)GetFieldOrLastInputField(i, data);
            }
            return answer;
        }
    }
}



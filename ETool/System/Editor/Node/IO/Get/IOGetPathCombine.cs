using System;
using System.IO;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/IO/Get/PathCombine")]
    public class IOGetPathCombine : NodeBase
    {
        public IOGetPathCombine(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Path Combine";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Path Count", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "First Path", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            FieldUpdate();
        }

        public override void FieldUpdate()
        {
            bool change = true;
            while (change)
            {
                change = false;
                if (fields.Count - 3 > (int)fields[1].GetValue(FieldType.Int))
                {
                    change = true;
                    SubField();
                }
                if (fields.Count - 3 < (int)fields[1].GetValue(FieldType.Int))
                {
                    change = true;
                    AddField();
                }
            }
        }

        private void AddField()
        {
            fields.Add(new Field(FieldType.String, "Other Path", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        private void SubField()
        {
            NodeBasedEditor.Instance.Connection_RemoveRelateConnectionInField(fields[fields.Count - 1]);
            fields.RemoveAt(fields.Count - 1);
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetAnswer(BlueprintInput data)
        {
            string answer = GetFieldOrLastInputField<string>(2, data);
            for (int i = 0; i < fields.Count - 3; i++)
            {
                answer = Path.Combine(answer, GetFieldOrLastInputField<string>(i + 3, data));
            }
            return answer;
        }
    }
}


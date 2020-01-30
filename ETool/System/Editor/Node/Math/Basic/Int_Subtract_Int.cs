using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Basic/Int - Int")]
    public class Int_Subtract_Int : NodeBase
    {
        public Int_Subtract_Int(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Int - Int";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Number Count", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "First Number", ConnectionType.DataInput, this, FieldContainer.Object));
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
            fields.Add(new Field(FieldType.Int, "Other Number", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        private void SubField()
        {
            NodeBasedEditor.Instance.RemoveRelateConnectionInField(fields[fields.Count - 1]);
            fields.RemoveAt(fields.Count - 1);
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetAnswer(BlueprintInput data)
        {
            int answer = (int)GetFieldOrLastInputField(2, data);
            for (int i = 0; i < fields.Count - 3; i++)
            {
                answer -= (int)GetFieldOrLastInputField(i + 3, data);
            }
            return answer;
        }
    }
}




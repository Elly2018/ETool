using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Basic/Float - Float")]
    public class Float_Subtract_Float : NodeBase
    {
        public Float_Subtract_Float(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Float - Float";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Number Count", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "First Number", ConnectionType.DataInput, this, FieldContainer.Object));
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
            fields.Add(new Field(FieldType.Float, "Other Number", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        private void SubField()
        {
            NodeBasedEditor.Instance.RemoveRelateConnectionInField(fields[fields.Count - 1]);
            fields.RemoveAt(fields.Count - 1);
        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetAnswer(BlueprintInput data)
        {
            float answer = (float)GetFieldOrLastInputField(2, data);
            for (int i = 0; i < fields.Count - 3; i++)
            {
                answer -= (float)GetFieldOrLastInputField(i + 3, data);
            }
            return answer;
        }
    }
}


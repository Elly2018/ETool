using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Basic/Vector3 * Vector3")]
    public class Vector3_Mul_Vector3 : NodeBase
    {
        public Vector3_Mul_Vector3(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Vector3 * Vector3";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Vector Count", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "First Vector", ConnectionType.DataInput, this, FieldContainer.Object));
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
                if (fields.Count - 3 > fields[1].target.target_Int)
                {
                    change = true;
                    SubField();
                }
                if (fields.Count - 3 < fields[1].target.target_Int)
                {
                    change = true;
                    AddField();
                }
            }
        }

        private void AddField()
        {
            fields.Add(new Field(FieldType.Vector3, "Other Vector", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        private void SubField()
        {
            NodeBasedEditor.Instance.RemoveRelateConnectionInField(fields[fields.Count - 1]);
            fields.RemoveAt(fields.Count - 1);
        }

        [NodePropertyGet(typeof(Vector3), 0)]
        public Vector3 GetAnswer(BlueprintInput data)
        {
            Vector3 answer = (Vector3)GetFieldOrLastInputField(2, data);
            for (int i = 0; i < fields.Count - 3; i++)
            {
                Vector3 buffer = (Vector3)GetFieldOrLastInputField(i + 3, data);
                answer = new Vector3(answer.x * buffer.x, answer.y * buffer.y, answer.z * buffer.z);
            }
            return answer;
        }
    }
}





using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Basic/Vector3 - Vector3")]
    public class Vector3_Subtract_Vector3 : NodeBase
    {
        public Vector3_Subtract_Vector3(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector3 - Vector3";
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
                answer -= (Vector3)GetFieldOrLastInputField(i + 3, data);
            }
            return answer;
        }
    }
}



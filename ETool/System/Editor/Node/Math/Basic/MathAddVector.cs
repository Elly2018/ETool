using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Basic/AddingVector")]
    [Math_Menu("Basic Vector")]
    public class MathAddVector : NodeBase
    {
        public MathAddVector(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector + Vector";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Vector Count", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Vector 0", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            FieldUpdate();
        }

        public override void FieldUpdate()
        {
            Update();
        }

        private void Update()
        {
            FieldType ft = (FieldType)(int)fields[0].GetValue(FieldType.Number);
            bool change = true;
            while (change)
            {
                change = false;
                if (fields.Count - 4 > (int)fields[2].GetValue(FieldType.Int))
                {
                    change = true;
                    DeleteLastField();
                }
                if (fields.Count - 4 < (int)fields[2].GetValue(FieldType.Int))
                {
                    change = true;
                    fields.Add(new Field(ft, "Vector " + (fields.Count - 3).ToString(), ConnectionType.DataInput, this, FieldContainer.Object));
                }
            }

            for (int i = 3; i < fields.Count; i++)
            {
                if (fields[i].fieldType != ft)
                {
                    EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[i]);
                    fields[i] = new Field(ft, "Vector " + (i - 3).ToString(), ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }

            if (fields[1].fieldType != ft)
            {
                EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[1]);
                fields[1] = new Field(ft, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object);
            }
        }

        [NodePropertyGet(typeof(object), 1)]
        public object GetAnswer(BlueprintInput data)
        {
            FieldType ft = (FieldType)(int)fields[0].GetValue(FieldType.Number);

            switch (ft)
            {
                case FieldType.Vector2:
                    {
                        Vector2 answer = (Vector2)GetFieldOrLastInputField(3, data);
                        for (int i = 4; i < fields.Count; i++)
                        {
                            answer += (Vector2)GetFieldOrLastInputField(i, data);
                        }
                        return answer;
                    }
                case FieldType.Vector3:
                    {
                        Vector3 answer = (Vector3)GetFieldOrLastInputField(3, data);
                        for (int i = 4; i < fields.Count; i++)
                        {
                            answer += (Vector3)GetFieldOrLastInputField(i, data);
                        }
                        return answer;
                    }
                case FieldType.Vector4:
                    {
                        Vector4 answer = (Vector4)GetFieldOrLastInputField(3, data);
                        for (int i = 4; i < fields.Count; i++)
                        {
                            answer += (Vector4)GetFieldOrLastInputField(i, data);
                        }
                        return answer;
                    }
            }
            return null;
        }
    }
}



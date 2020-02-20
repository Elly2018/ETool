using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Basic/DivideNumber")]
    [Math_Menu("Basic Number")]
    public class MathDivNumber : NodeBase
    {
        public MathDivNumber(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Number ÷ Number";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Number, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Number Count", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Number 0", ConnectionType.DataInput, this, FieldContainer.Object));
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
                    fields.Add(new Field(ft, "Number " + (fields.Count - 3).ToString(), ConnectionType.DataInput, this, FieldContainer.Object));
                }
            }

            for (int i = 3; i < fields.Count; i++)
            {
                if (fields[i].fieldType != ft)
                {
                    EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[i]);
                    fields[i] = new Field(ft, "Number " + (i - 3).ToString(), ConnectionType.DataInput, this, FieldContainer.Object);
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
                case FieldType.Int:
                    {
                        int answer = (int)GetFieldOrLastInputField(3, data);
                        for (int i = 4; i < fields.Count; i++)
                        {
                            answer /= (int)GetFieldOrLastInputField(i, data);
                        }
                        return answer;
                    }
                case FieldType.Long:
                    {
                        long answer = (long)GetFieldOrLastInputField(3, data);
                        for (int i = 4; i < fields.Count; i++)
                        {
                            answer /= (long)GetFieldOrLastInputField(i, data);
                        }
                        return answer;
                    }
                case FieldType.Float:
                    {
                        float answer = (float)GetFieldOrLastInputField(3, data);
                        for (int i = 4; i < fields.Count; i++)
                        {
                            answer /= (float)GetFieldOrLastInputField(i, data);
                        }
                        return answer;
                    }
                case FieldType.Double:
                    {
                        double answer = (double)GetFieldOrLastInputField(3, data);
                        for (int i = 4; i < fields.Count; i++)
                        {
                            answer /= (double)GetFieldOrLastInputField(i, data);
                        }
                        return answer;
                    }
            }
            return null;
        }
    }
}



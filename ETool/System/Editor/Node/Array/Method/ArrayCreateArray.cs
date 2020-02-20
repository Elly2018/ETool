using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Method/CreateArray")]
    public class ArrayCreateArray : NodeBase
    {
        public ArrayCreateArray(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Create Array";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Size", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(Array), 2)]
        public object[] GetArray(BlueprintInput data)
        {
            List<object> _arg = new List<object>();
            FieldType ft = (FieldType)fields[0].GetValue(FieldType.Type);

            for (int i = 3; i < fields.Count; i++)
            {
                _arg.Add(fields[i].GetValue(ft));
            }

            return _arg.ToArray();
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            FieldUpdate();
        }

        public override void FieldUpdate()
        {
            FieldType ft = (FieldType)fields[0].GetValue(FieldType.Type);
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
                    AddField(ft);
                }
            }

            if(fields[2].fieldType != ft)
            {
                fields[2] = new Field(ft, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Array);
            }

            for(int i = 3; i < fields.Count; i++)
            {
                if(fields[i].fieldType != ft)
                {
                    EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[i]);
                    Field buffer = new Field(ft, "Element: ", ConnectionType.DataInput, this, FieldContainer.Object);
                    fields[i] = buffer;
                    buffer.title = "Element: " + (fields.IndexOf(buffer) - 3).ToString();
                }
            }
        }

        private void AddField(FieldType ft)
        {
            Field buffer = new Field(ft, "Element: ", ConnectionType.DataInput, this, FieldContainer.Object);
            fields.Add(buffer);
            buffer.title = "Element: " + (fields.IndexOf(buffer) - 4).ToString();
        }

        private void SubField()
        {
            EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[fields.Count - 1]);
            fields.RemoveAt(fields.Count - 1);
        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetAnswer(BlueprintInput data)
        {
            float answer = (float)GetFieldOrLastInputField(2, data);
            for (int i = 0; i < fields.Count - 3; i++)
            {
                answer += (float)GetFieldOrLastInputField(i + 3, data);
            }
            return answer;
        }
    }
}

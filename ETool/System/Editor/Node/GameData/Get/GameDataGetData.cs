using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameData/Get/GetData")]
    public class GameDataGetData : NodeBase
    {
        public GameDataGetData(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Data";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameData, "Target", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Category", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Element", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(object), 4)]
        public object GetResult(BlueprintInput data)
        {
            EGameData target = fields[0].target.genericUnityType.target_GameData;
            if (target != null)
            {
                string v1 = (string)GetFieldOrLastInputField(1, data);
                string v2 = (string)GetFieldOrLastInputField(2, data);
                FieldType v3 = (FieldType)GetFieldOrLastInputField(3, data);

                return target.GetData(v1, v2, v3);
            }
            Debug.LogWarning("Game data select is null");
            return null;
        }

        public override void FinalFieldInitialize(BlueprintInput data)
        {
            UpdateFieldType();
        }

        public override void FieldUpdate()
        {
            UpdateFieldType();
        }

        private void UpdateFieldType()
        {
            FieldType ty = (FieldType)(int)fields[3].GetValue(FieldType.Type);
            if (fields[4].fieldType != ty)
            {
                fields[4] = new Field(ty, "Value", ConnectionType.DataOutput, true, this, FieldContainer.Object);
            }
        }
    }
}

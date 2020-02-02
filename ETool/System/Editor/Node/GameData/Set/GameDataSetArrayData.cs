using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameData/Set/SetArrayData")]
    public class GameDataSetArrayData : NodeBase
    {
        public GameDataSetArrayData(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Array Data";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameData, "Target", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Category", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Element", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Value", ConnectionType.DataInput, this, FieldContainer.Array));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            EGameData target = fields[1].target.target_Component.gameData;

            if (target != null)
            {
                string v1 = (string)GetFieldOrLastInputField(2, data);
                string v2 = (string)GetFieldOrLastInputField(3, data);
                object[] v3 = (object[])GetFieldOrLastInputField(5, data);
                FieldType v4 = (FieldType)GetFieldOrLastInputField(4, data);

                target.SetData(v1, v2, v3, v4);
            }

            ActiveNextEvent(0, data);
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
            FieldType ty = (FieldType)(int)fields[4].GetValue(FieldType.Type);
            if (fields[5].fieldType != ty)
            {
                fields[5] = new Field(ty, "Value", ConnectionType.DataInput, this, FieldContainer.Array);
            }
        }
    }
}

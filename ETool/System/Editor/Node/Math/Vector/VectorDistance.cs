using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Vector/VectorDistance")]
    [Math_Menu("Vector")]
    public class VectorDistance : NodeBase
    {
        public VectorDistance(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector Distance";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector2, "First", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector2, "Second", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            TypeUpdate();
        }

        public override void FieldUpdate()
        {
            TypeUpdate();
        }

        private void TypeUpdate()
        {
            FieldType ft = (FieldType)fields[0].GetValue(FieldType.Vector);
            if (fields[2].fieldType != ft)
            {
                EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[2]);
                fields[2] = new Field(ft, "First", ConnectionType.DataInput, this, FieldContainer.Object);
            }
            if (fields[3].fieldType != ft)
            {
                EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[3]);
                fields[3] = new Field(ft, "Second", ConnectionType.DataInput, this, FieldContainer.Object);
            }
        }

        [NodePropertyGet(typeof(float), 1)]
        public float GetResultVector2(BlueprintInput data)
        {
            FieldType ft = (FieldType)fields[0].GetValue(FieldType.Vector);
            switch (ft)
            {
                case FieldType.Vector2:
                    return Vector2.Distance((Vector2)GetFieldOrLastInputField(2, data), (Vector2)GetFieldOrLastInputField(3, data));
                case FieldType.Vector3:
                    return Vector3.Distance((Vector3)GetFieldOrLastInputField(2, data), (Vector3)GetFieldOrLastInputField(3, data));
                case FieldType.Vector4:
                    return Vector4.Distance((Vector4)GetFieldOrLastInputField(2, data), (Vector4)GetFieldOrLastInputField(3, data));
            }
            return Vector2.Distance((Vector2)GetFieldOrLastInputField(2, data), (Vector2)GetFieldOrLastInputField(3, data));
        }
    }
}


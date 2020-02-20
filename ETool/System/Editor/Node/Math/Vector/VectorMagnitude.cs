using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Vector/VectorMagnitude")]
    [Math_Menu("Vector")]
    public class VectorMagnitude : NodeBase
    {
        public VectorMagnitude(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector Magnitude";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector2, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
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
                fields[2] = new Field(ft, "Value", ConnectionType.DataInput, this, FieldContainer.Object);
            }
        }

        [NodePropertyGet(typeof(float), 1)]
        public float GetResultVector2(BlueprintInput data)
        {
            FieldType ft = (FieldType)fields[0].GetValue(FieldType.Vector);
            switch (ft)
            {
                case FieldType.Vector2:
                    return GetFieldOrLastInputField<Vector2>(2, data).magnitude;
                case FieldType.Vector3:
                    return GetFieldOrLastInputField<Vector3>(2, data).magnitude;
                case FieldType.Vector4:
                    return GetFieldOrLastInputField<Vector4>(2, data).magnitude;
            }
            return GetFieldOrLastInputField<Vector2>(2, data).magnitude;
        }
    }
}


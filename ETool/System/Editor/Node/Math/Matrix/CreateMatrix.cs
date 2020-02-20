using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Matrix/CreateMatrix")]
    [Math_Menu("Matrix")]
    public class CreateMatrix : NodeBase
    {
        public CreateMatrix(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Create Matrix";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Matrix4x4, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector4, "Row 1", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector4, "Row 2", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector4, "Row 3", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector4, "Row 4", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Matrix4x4), 0)]
        public Matrix4x4 GetResult(BlueprintInput data)
        {
            Matrix4x4 result = new Matrix4x4();
            result.SetRow(0, GetFieldOrLastInputField<Vector4>(1, data));
            result.SetRow(1, GetFieldOrLastInputField<Vector4>(2, data));
            result.SetRow(2, GetFieldOrLastInputField<Vector4>(3, data));
            result.SetRow(3, GetFieldOrLastInputField<Vector4>(4, data));
            return result;
        }
    }
}

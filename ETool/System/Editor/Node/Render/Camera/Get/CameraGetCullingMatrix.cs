using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Camera/Get/GetCullingMatrix")]
    public class CameraGetCullingMatrix : NodeBase
    {
        public CameraGetCullingMatrix(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Culling Matrix";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Matrix4x4, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Camera, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Matrix4x4), 0)]
        public Matrix4x4 GetMatrixResult(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Camera>(1, data).cullingMatrix;
        }
    }
}

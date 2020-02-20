using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Camera/Get/GetCameraType")]
    public class CameraGetCameraType : NodeBase
    {
        public CameraGetCameraType(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Camera Type";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.CameraType, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Camera, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(CameraType), 0)]
        public CameraType GetMatrixResult(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Camera>(1, data).cameraType;
        }
    }
}

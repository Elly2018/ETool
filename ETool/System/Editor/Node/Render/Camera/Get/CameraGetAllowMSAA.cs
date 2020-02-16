using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Camera/Get/GetAllowMSAA")]
    public class CameraGetAllowMSAA : NodeBase
    {
        public CameraGetAllowMSAA(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Allow MSAA";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Camera, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ConnectionUpdate()
        {
            NodeError nodeError = new NodeError() { errorType = NodeErrorType.ConnectionError, errorString = "The Target field must link a camera" };
            bool gameObjectConnection = NodeBasedEditor.Instance.Check_ConnectionExist(this, 1, true);

            if (!gameObjectConnection)
            {
                AddNodeError(nodeError);
            }
            else
            {
                DeleteNodeError(nodeError);
            }
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetID(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Camera>(1, data).allowMSAA;
        }
    }
}

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Camera/Get/GetClearFlags")]
    public class CameraGetClearFlags : NodeBase
    {
        public CameraGetClearFlags(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Camera ClearFlags";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.CameraClearFlags, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Camera, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ConnectionUpdate()
        {
            NodeError nodeError = new NodeError() { errorType = NodeErrorType.ConnectionError, errorString = "The Target field must link a camera" };
            bool gameObjectConnection = EBlueprint.GetBlueprintByNode(this).Check_ConnectionExist(this, 1, true);

            if (!gameObjectConnection)
            {
                AddNodeError(nodeError);
            }
            else
            {
                DeleteNodeError(nodeError);
            }
        }

        [NodePropertyGet(typeof(CameraClearFlags), 0)]
        public CameraClearFlags GetID(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Camera>(1, data).clearFlags;
        }
    }
}

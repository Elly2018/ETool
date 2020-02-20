using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Camera/Method/Reset")]
    public class CameraReset : NodeBase
    {
        public CameraReset(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Camera Reset";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Camera, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<Camera>(1, data).Reset();
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

        [NodePropertyGet(typeof(Camera), 1)]
        public Camera GetID(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Camera>(1, data);
        }
    }
}

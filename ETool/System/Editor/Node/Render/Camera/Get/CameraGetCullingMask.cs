using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Camera/Get/GetCullingMask")]
    public class CameraGetCullingMask : NodeBase
    {
        public CameraGetCullingMask(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Camera CullingMask";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
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

        [NodePropertyGet(typeof(int), 0)]
        public int GetID(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Camera>(1, data).cullingMask;
        }
    }
}

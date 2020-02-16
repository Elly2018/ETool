using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Camera/Get/GetBackgroundColor")]
    public class CameraGetBackgroundColor : NodeBase
    {
        public CameraGetBackgroundColor(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Camera Background Color";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Color, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
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

        [NodePropertyGet(typeof(Color), 0)]
        public Color GetID(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Camera>(1, data).backgroundColor;
        }
    }
}

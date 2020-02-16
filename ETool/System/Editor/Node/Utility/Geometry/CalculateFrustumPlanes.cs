using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Utility/Geometry/CalculateFrustumPlanes")]
    public class CalculateFrustumPlanes : NodeBase
    {
        public CalculateFrustumPlanes(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Calculate Frustum Planes";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Plane, "Plane Array", ConnectionType.DataOutput, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.Camera, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ConnectionUpdate()
        {
            NodeError nodeError = new NodeError() { errorString = "This node need a target camera input", errorType = NodeErrorType.ConnectionError };
            bool exist = NodeBasedEditor.Instance.Check_ConnectionExist(this, 1, true);
            if (!exist)
            {
                AddNodeError(nodeError);
            }
            else
            {
                DeleteNodeError(nodeError);
            }
        }

        [NodePropertyGet(typeof(Plane[]), 0)]
        public Plane[] GetPlanes(BlueprintInput data)
        {
            return GeometryUtility.CalculateFrustumPlanes(GetFieldOrLastInputField<Camera>(1, data));
        }
    }
}

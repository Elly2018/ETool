using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Utility/Geometry/CalculateBounds")]
    public class CalculateBounds : NodeBase
    {
        public CalculateBounds(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Calculate Bounds";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Bounds, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Positions", ConnectionType.DataInput, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.Matrix4x4, "Transform", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ConnectionUpdate()
        {
            NodeError nodeError1 = new NodeError() { errorString = "This node need a target Vector3 position array input", errorType = NodeErrorType.ConnectionError, code = 1 };
            NodeError nodeError2 = new NodeError() { errorString = "This node need a target Matrix4x4 input", errorType = NodeErrorType.ConnectionError, code = 2 };
            bool exist1 = NodeBasedEditor.Instance.Check_ConnectionExist(this, 1, true);
            bool exist2 = NodeBasedEditor.Instance.Check_ConnectionExist(this, 2, true);

            if (!exist1)
            {
                AddNodeError(nodeError1);
            }
            else
            {
                DeleteNodeError(nodeError1);
            }

            if (!exist2)
            {
                AddNodeError(nodeError2);
            }
            else
            {
                DeleteNodeError(nodeError2);
            }
        }

        [NodePropertyGet(typeof(Bounds), 0)]
        public Bounds GetPlanes(BlueprintInput data)
        {
            return GeometryUtility.CalculateBounds(GetFieldOrLastInputField<Vector3[]>(1, data), GetFieldOrLastInputField<Matrix4x4>(2, data));
        }
    }
}

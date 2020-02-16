using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Utility/Geometry/TestPlanesAABB")]
    public class TestPlanesAABB : NodeBase
    {
        public TestPlanesAABB(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Test Planes AABB";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Plane, "Plane Array", ConnectionType.DataInput, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.Bounds, "Bound", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ConnectionUpdate()
        {
            NodeError nodeError1 = new NodeError() { errorString = "This node need a target Plane Array input", errorType = NodeErrorType.ConnectionError, code = 1 };
            NodeError nodeError2 = new NodeError() { errorString = "This node need a target Bounds input", errorType = NodeErrorType.ConnectionError, code = 2 };
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

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetPlanes(BlueprintInput data)
        {
            return GeometryUtility.TestPlanesAABB(GetFieldOrLastInputField<Plane[]>(1, data), GetFieldOrLastInputField<Bounds>(2, data));
        }
    }
}

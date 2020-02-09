using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Physics/Ray/Method/CreateRay")]
    public class RayCreateRay : NodeBase
    {
        public RayCreateRay(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Create Ray";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Ray, "Result", ConnectionType.DataOutput, false, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Position", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Direction", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Ray), 0)]
        public Ray GetRay(BlueprintInput data)
        {
            return new Ray(GetFieldOrLastInputField<Vector3>(1, data), GetFieldOrLastInputField<Vector3>(2, data));
        }
    }
}

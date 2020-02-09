using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Physics/Ray/Get/GetPoint")]
    public class RayGetRayPoint : NodeBase
    {
        public RayGetRayPoint(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Ray Point";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, false, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Ray, "Ray", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Distance", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 0)]
        public Vector3 GetP(BlueprintInput data)
        {
            Ray r = GetFieldOrLastInputField<Ray>(1, data);
            float f = GetFieldOrLastInputField<float>(2, data);
            return r.GetPoint(f);
        }
    }
}

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Constant/Vector3")]
    public class Constant_Vector3 : NodeBase
    {
        public Constant_Vector3(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Constant Vector3";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector3, "Vector3", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 0)]
        public Vector3 GetVector3(BlueprintInput data)
        {
            return (Vector3)Field.GetObjectByFieldType(FieldType.Vector3, fields[0].target);
        }
    }
}

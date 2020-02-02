using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Constant/Vector2")]
    public class Constant_Vector2 : NodeBase
    {
        public Constant_Vector2(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Vector2";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector2, "Vector2", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 0)]
        public Vector2 GetVector3(BlueprintInput data)
        {
            return (Vector2)Field.GetObjectByFieldType(FieldType.Vector2, fields[0].target);
        }
    }
}

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Constant/Vector4")]
    [Constant_Menu(1)]
    public class Constant_Vector4 : NodeBase
    {
        public Constant_Vector4(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Vector4";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector4, "Vector4", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector4), 0)]
        public Vector4 GetVector4(BlueprintInput data)
        {
            return (Vector4)Field.GetObjectByFieldType(FieldType.Vector4, fields[0].target);
        }
    }
}

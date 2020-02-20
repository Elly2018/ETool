using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Constant/Texture3D")]
    [Constant_Menu]
    public class Constant_Texture3D : NodeBase
    {
        public Constant_Texture3D(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Texture3D";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Texture3D, "Texture3D", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Texture3D), 0)]
        public Texture3D GetVector4(BlueprintInput data)
        {
            return (Texture3D)Field.GetObjectByFieldType(FieldType.Texture3D, fields[0].target);
        }
    }
}
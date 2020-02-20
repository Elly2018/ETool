using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Constant/Mesh")]
    [Constant_Menu]
    public class Constant_Mesh : NodeBase
    {
        public Constant_Mesh(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Mesh";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Mesh, "Mesh", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Mesh), 0)]
        public Mesh GetVector4(BlueprintInput data)
        {
            return (Mesh)Field.GetObjectByFieldType(FieldType.Mesh, fields[0].target);
        }
    }
}


using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Get/GetMaterialByMeshRenderer")]
    public class MaterialGetMaterialByMeshRenderer : NodeBase
    {
        public MaterialGetMaterialByMeshRenderer(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Material By Mesh Renderer";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.MeshRenderer, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Material, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Material), 1)]
        public Material GetMat(BlueprintInput data)
        {
            return GetFieldOrLastInputField<MeshRenderer>(0, data).material;
        }
    }
}

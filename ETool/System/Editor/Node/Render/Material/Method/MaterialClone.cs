using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Method/Clone")]
    public class MaterialClone : NodeBase
    {
        public MaterialClone(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Material Clone";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Material), 0)]
        public Material GetMat(BlueprintInput data)
        {
            return Material.Instantiate(GetFieldOrLastInputField<Material>(0, data));
        }
    }
}

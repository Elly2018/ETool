using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Method/Lerp")]
    public class MaterialLerp : NodeBase
    {
        public MaterialLerp(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Material Lerp";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Material, "Start", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Material, "End", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Fac", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Material mat = GetFieldOrLastInputField<Material>(1, data);
            Material mat1 = GetFieldOrLastInputField<Material>(2, data);
            Material mat2 = GetFieldOrLastInputField<Material>(3, data);
            float fac = GetFieldOrLastInputField<float>(4, data);

            if (mat1 != null && mat2 != null && mat != null)
                mat.Lerp(mat1, mat2, fac);

            ActiveNextEvent(0, data);
        }

        [NodePropertyGet(typeof(Material), 1)]
        public Material GetMat1(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(1, data);
        }

        [NodePropertyGet(typeof(Material), 2)]
        public Material GetMat2(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(2, data);
        }

        [NodePropertyGet(typeof(Material), 3)]
        public Material GetMat3(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(3, data);
        }
    }
}

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Method/HasProperty")]
    public class MaterialHasProperty : NodeBase
    {
        public MaterialHasProperty(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Material Has Property";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Property", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetMat(BlueprintInput data)
        {
            string v = GetFieldOrLastInputField<string>(2, data);
            return GetFieldOrLastInputField<Material>(1, data).HasProperty(v);
        }
    }
}

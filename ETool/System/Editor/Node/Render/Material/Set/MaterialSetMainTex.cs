using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Set/SetMainTexture")]
    public class MaterialSetMainTexture : NodeBase
    {
        public MaterialSetMainTexture(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Material Set Main Texture";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Texture, "Texture", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Material mat = GetFieldOrLastInputField<Material>(1, data);
            Texture tex = GetFieldOrLastInputField<Texture>(2, data);

            if (mat != null)
                mat.mainTexture = tex;

            ActiveNextEvent(0, data);
        }

        [NodePropertyGet(typeof(Material), 1)]
        public Material GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(1, data);
        }
    }
}

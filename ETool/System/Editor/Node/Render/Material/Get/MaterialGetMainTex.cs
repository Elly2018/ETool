using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Get/GetMainTexture")]
    public class MaterialGetMainTex : NodeBase
    {
        public MaterialGetMainTex(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Material Main Texture";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Texture, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Material), 0)]
        public Material GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(0, data);
        }

        [NodePropertyGet(typeof(Texture), 1)]
        public Texture GetLightColor(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(0, data).mainTexture;
        }
    }
}

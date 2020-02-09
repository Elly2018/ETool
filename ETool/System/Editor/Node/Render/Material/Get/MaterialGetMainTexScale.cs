using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Get/GetMainTextureScale")]
    public class MaterialGetMainTexScale : NodeBase
    {
        public MaterialGetMainTexScale(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Material Main Texture Scale";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector2, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Material), 0)]
        public Material GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(0, data);
        }

        [NodePropertyGet(typeof(Vector2), 1)]
        public Vector2 GetLightColor(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(0, data).mainTextureScale;
        }
    }
}

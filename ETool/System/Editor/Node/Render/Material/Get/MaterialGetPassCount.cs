using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Get/GetPassCount")]
    public class MaterialGetPassCount : NodeBase
    {
        public MaterialGetPassCount(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Material Main Texture Scale";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Material), 0)]
        public Material GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(0, data);
        }

        [NodePropertyGet(typeof(int), 1)]
        public int GetLightColor(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(0, data).passCount;
        }
    }
}

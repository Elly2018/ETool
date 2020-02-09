using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Get/GetMainColor")]
    public class MaterialGetMainColor : NodeBase
    {
        public MaterialGetMainColor(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Material Main Color";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Color, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Material), 0)]
        public Material GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(0, data);
        }

        [NodePropertyGet(typeof(Color), 1)]
        public Color GetLightColor(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(0, data).color;
        }
    }
}

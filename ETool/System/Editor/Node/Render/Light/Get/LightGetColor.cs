using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Light/Get/GetColor")]
    public class LightGetColor : NodeBase
    {
        public LightGetColor(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Light Color";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Light, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Color, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Light), 0)]
        public Light GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Light>(0, data);
        }

        [NodePropertyGet(typeof(Color), 1)]
        public Color GetLightColor(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Light>(0, data).color;
        }
    }
}

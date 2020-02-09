using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Light/Get/GetIntensity")]
    public class LightGetIntensity : NodeBase
    {
        public LightGetIntensity(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Light Intensity";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Light, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Light), 0)]
        public Light GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Light>(0, data);
        }

        [NodePropertyGet(typeof(float), 1)]
        public float GetLightColor(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Light>(0, data).intensity;
        }
    }
}

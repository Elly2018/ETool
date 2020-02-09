using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Light/Get/GetShadowType")]
    public class LightGetShadowType : NodeBase
    {
        public LightGetShadowType(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Shadow Type";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Light, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.ShadowType, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Light), 0)]
        public Light GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Light>(0, data);
        }

        [NodePropertyGet(typeof(LightShadows), 1)]
        public LightShadows GetLightColor(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Light>(0, data).shadows;
        }
    }
}

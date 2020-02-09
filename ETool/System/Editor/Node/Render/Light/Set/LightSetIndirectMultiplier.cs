using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Light/Set/SetIndirectMultiplier")]
    public class LightSetIndirectMultiplier : NodeBase
    {
        public LightSetIndirectMultiplier(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Light Indirect Multiplier";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Light l = GetFieldOrLastInputField<Light>(1, data);
            float v1 = GetFieldOrLastInputField<float>(2, data);

            if (l != null)
                l.bounceIntensity = v1;

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Light, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Light), 1)]
        public Light GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Light>(1, data);
        }
    }
}

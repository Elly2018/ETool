using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Light/Set/SetShadowType")]
    public class LightSetShadowType : NodeBase
    {
        public LightSetShadowType(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Shadow Type";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Light l = GetFieldOrLastInputField<Light>(1, data);
            LightShadows v1 = GetFieldOrLastInputField<LightShadows>(2, data);

            if (l != null)
                l.shadows = v1;

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Light, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.ShadowType, "Type", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Light), 1)]
        public Light GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Light>(1, data);
        }
    }
}

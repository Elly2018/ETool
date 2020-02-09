using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Light/Set/SetColor")]
    public class LightSetColor : NodeBase
    {
        public LightSetColor(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Light Color";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Light l = GetFieldOrLastInputField<Light>(1, data);
            Color v1 = GetFieldOrLastInputField<Color>(2, data);

            if (l != null)
                l.color = v1;

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Light, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Color, "Color", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Light), 1)]
        public Light GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Light>(1, data);
        }
    }
}

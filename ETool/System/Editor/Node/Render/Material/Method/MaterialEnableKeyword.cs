﻿using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Method/EnableKeyword")]
    public class MaterialEnableKeyword : NodeBase
    {
        public MaterialEnableKeyword(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Material Enable Keyword";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Keyword", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Material mat = GetFieldOrLastInputField<Material>(1, data);
            string key = GetFieldOrLastInputField<string>(2, data);

            if (mat != null)
                mat.EnableKeyword(key);

            ActiveNextEvent(0, data);
        }

        [NodePropertyGet(typeof(Material), 1)]
        public Material GetMat(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(1, data);
        }
    }
}

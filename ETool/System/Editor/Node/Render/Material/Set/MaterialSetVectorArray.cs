﻿using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Set/SetVectorArray")]
    public class MaterialSetVectorArray : NodeBase
    {
        public MaterialSetVectorArray(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Material Set Vector Array";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Use ID", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector4, "Vector", ConnectionType.DataInput, this, FieldContainer.Array));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Material mat = GetFieldOrLastInputField<Material>(1, data);
            Vector4[] Value = GetFieldOrLastInputField<Vector4[]>(4, data);

            if ((bool)fields[2].GetValue(FieldType.Boolean))
            {
                int id = GetFieldOrLastInputField<int>(3, data);

                if (mat != null)
                    mat.SetVectorArray(id, Value);
            }
            else
            {
                string name = GetFieldOrLastInputField<string>(3, data);
                if (mat != null)
                    mat.SetVectorArray(name, Value);
            }

            ActiveNextEvent(0, data);
        }

        [NodePropertyGet(typeof(Material), 1)]
        public Material GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(1, data);
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            TypeUpdate();
        }

        public override void FieldUpdate()
        {
            TypeUpdate();
        }

        private void TypeUpdate()
        {
            if ((bool)fields[2].GetValue(FieldType.Boolean))
            {
                if (fields[3].fieldType != FieldType.Int)
                {
                    EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[3]);
                    fields[3] = new Field(FieldType.Int, "ID", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
            else
            {
                if (fields[3].fieldType != FieldType.String)
                {
                    EBlueprint.GetBlueprintByNode(this).Connection_RemoveRelateConnectionInField(fields[3]);
                    fields[3] = new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
        }
    }
}

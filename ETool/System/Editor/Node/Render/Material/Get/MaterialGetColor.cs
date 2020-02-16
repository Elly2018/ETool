using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Get/GetColor")]
    public class MaterialGetColor : NodeBase
    {
        public MaterialGetColor(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Material Color";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Color, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Use ID", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
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
            if((bool)fields[2].GetValue(FieldType.Boolean))
            {
                if (fields[3].fieldType != FieldType.Int)
                {
                    NodeBasedEditor.Instance.Connection_RemoveRelateConnectionInField(fields[3]);
                    fields[3] = new Field(FieldType.Int, "ID", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
            else
            {
                if (fields[3].fieldType != FieldType.String)
                {
                    NodeBasedEditor.Instance.Connection_RemoveRelateConnectionInField(fields[3]);
                    fields[3] = new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
        }

        [NodePropertyGet(typeof(Material), 0)]
        public Material GetLight(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Material>(0, data);
        }

        [NodePropertyGet(typeof(Color), 1)]
        public Color GetLightColor(BlueprintInput data)
        {
            if ((bool)fields[2].GetValue(FieldType.Boolean))
            {
                return GetFieldOrLastInputField<Material>(0, data).GetColor(GetFieldOrLastInputField<int>(3, data));
            }
            else
            {
                return GetFieldOrLastInputField<Material>(0, data).GetColor(GetFieldOrLastInputField<string>(3, data));
            }
        }
    }
}

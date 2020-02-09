using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Material/Set/SetTexture")]
    public class MaterialSetTexture : NodeBase
    {
        public MaterialSetTexture(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Material Set Texture";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Material, "Target", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Use ID", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Texture, "Texture", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Material mat = GetFieldOrLastInputField<Material>(1, data);
            Texture Value = GetFieldOrLastInputField<Texture>(4, data);

            if ((bool)fields[2].GetValue(FieldType.Boolean))
            {
                int id = GetFieldOrLastInputField<int>(3, data);

                if (mat != null)
                    mat.SetTexture(id, Value);
            }
            else
            {
                string name = GetFieldOrLastInputField<string>(3, data);
                if (mat != null)
                    mat.SetTexture(name, Value);
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
                    NodeBasedEditor.Instance.RemoveRelateConnectionInField(fields[3]);
                    fields[3] = new Field(FieldType.Int, "ID", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
            else
            {
                if (fields[3].fieldType != FieldType.String)
                {
                    NodeBasedEditor.Instance.RemoveRelateConnectionInField(fields[3]);
                    fields[3] = new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
        }
    }
}

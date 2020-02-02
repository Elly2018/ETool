using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Get/GetFloat")]
    public class AnimatorGetFloat : NodeBase
    {
        public AnimatorGetFloat(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Float";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Use ID", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 3)]
        public float GetResult(BlueprintInput data)
        {
            bool v1 = GetFieldOrLastInputField<bool>(0, data);
            Animator v3 = GetFieldOrLastInputField<Animator>(2, data);

            if (v3 == null) return -1;

            if (v1)
            {
                int id = GetFieldOrLastInputField<int>(1, data);
                return v3.GetFloat(id);
            }
            else
            {
                string name = GetFieldOrLastInputField<string>(1, data);
                return v3.GetFloat(name);
            }
        }

        public override void FieldUpdate()
        {
            Update();
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            Update();
        }

        private void Update()
        {
            bool useID = (bool)fields[0].GetValue(FieldType.Boolean);
            if (useID)
            {
                if (fields[1].fieldType != FieldType.Int)
                {
                    fields[1] = new Field(FieldType.Int, "ID", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
            else
            {
                if (fields[1].fieldType != FieldType.String)
                {
                    fields[1] = new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
        }
    }
}


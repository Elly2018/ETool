using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Method/ResetTrigger")]
    public class AnimatorResetTrigger : NodeBase
    {
        public AnimatorResetTrigger(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Reset Trigger";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Use ID", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            bool v1 = GetFieldOrLastInputField<bool>(1, data);
            Animator v3 = GetFieldOrLastInputField<Animator>(3, data);

            if (v3 != null)
            {
                if (v1)
                {
                    int id = GetFieldOrLastInputField<int>(2, data);
                    v3.ResetTrigger(id);
                }
                else
                {
                    string name = GetFieldOrLastInputField<string>(2, data);
                    v3.ResetTrigger(name);
                }
            }

            ActiveNextEvent(0, data);
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
            bool useID = (bool)fields[1].GetValue(FieldType.Boolean);
            if (useID)
            {
                if (fields[2].fieldType != FieldType.Int)
                {
                    fields[2] = new Field(FieldType.Int, "ID", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
            else
            {
                if (fields[2].fieldType != FieldType.String)
                {
                    fields[2] = new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
        }
    }
}

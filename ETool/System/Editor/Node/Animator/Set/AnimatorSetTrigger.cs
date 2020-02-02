using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Set/SetTrigger")]
    public class AnimatorSetTrigger : NodeBase
    {
        public AnimatorSetTrigger(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Trigger";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AvatarIKGoal, "Goal", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Use ID", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            AvatarIKGoal v1 = GetFieldOrLastInputField<AvatarIKGoal>(1, data);
            Animator v3 = GetFieldOrLastInputField<Animator>(4, data);

            if (v3)
            {
                int id = GetFieldOrLastInputField<int>(3, data);
                v3.SetTrigger(id);
            }
            else
            {
                string name = GetFieldOrLastInputField<string>(3, data);
                v3.SetTrigger(name);
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
            bool useID = (bool)fields[2].GetValue(FieldType.Boolean);
            if (useID)
            {
                if (fields[3].fieldType != FieldType.Int)
                {
                    fields[3] = new Field(FieldType.Int, "ID", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
            else
            {
                if (fields[3].fieldType != FieldType.String)
                {
                    fields[3] = new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object);
                }
            }
        }
    }
}

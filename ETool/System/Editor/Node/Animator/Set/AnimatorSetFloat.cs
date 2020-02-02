using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Set/SetFloat")]
    public class AnimatorSetFloat : NodeBase
    {
        public AnimatorSetFloat(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Float";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Use Transition", ConnectionType.None, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Animator a = (Animator)GetFieldOrLastInputField(1, data);
            string n = (string)GetFieldOrLastInputField(2, data);
            float v = (float)GetFieldOrLastInputField(3, data);

            if (a != null)
            {
                a.SetFloat(n, v);
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
            bool useTransition = (bool)fields[4].GetValue(FieldType.Boolean);
            if (useTransition)
            {
                if(fields.Count == 5)
                {
                    fields.Add(new Field(FieldType.Float, "Damp Time", ConnectionType.DataInput, this, FieldContainer.Object));
                    fields.Add(new Field(FieldType.Float, "Delta Time", ConnectionType.DataInput, this, FieldContainer.Object));
                }
            }
            else
            {
                while(fields.Count != 5)
                {
                    DeleteLastField();
                }
            }
        }
    }
}



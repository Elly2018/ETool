using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/ForEach Element")]
    public class ForEach_Element : NodeBase
    {
        private object buffer = null;

        public ForEach_Element(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "For Each";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            int count = GetFieldOrLastInputField<object[]>(3, data).Length;

            for (int i = 0; i < count; i++)
            {
                buffer = GetFieldOrLastInputField<object[]>(3, data)[i];
                ActiveNextEvent(0, data);
            }

            ActiveNextEvent(4, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Select", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Array", ConnectionType.DataInput, true, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.Event, "Finish", ConnectionType.EventOutput, this, FieldContainer.Object));
        }

        public override void FieldUpdate()
        {
            FieldUpdateType();
        }

        public override void FinalFieldInitialize(BlueprintInput data)
        {
            FieldUpdateType();
        }

        private void FieldUpdateType()
        {
            FieldType ft = (FieldType)fields[1].GetValue(FieldType.Type);
            if (fields[2].fieldType != ft)
            {
                fields[2] = new Field(ft, "Select", ConnectionType.DataOutput, true, this, FieldContainer.Object);
            }
            if (fields[3].fieldType != ft)
            {
                fields[3] = new Field(ft, "Array", ConnectionType.DataInput, true, this, FieldContainer.Array);
            }
        }

        [NodePropertyGet(typeof(object), 2)]
        public object GetIndex(BlueprintInput data)
        {
            return buffer;
        }
    }
}


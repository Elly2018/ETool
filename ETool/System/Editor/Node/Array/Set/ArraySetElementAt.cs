using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Set/SetElementAt")]
    public class ArraySetElementAt : NodeBase
    {
        private object[] buffer;

        public ArraySetElementAt(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Element At";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Index", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Target", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Array", ConnectionType.DataBoth, true, this, FieldContainer.Array));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            int index = GetFieldOrLastInputField<int>(2, data);
            object target = GetFieldOrLastInputField<object>(3, data);
            object[] array = GetFieldOrLastInputField<object[]>(4, data);

            array[index] = target;
            buffer = array;

            ActiveNextEvent(0, data);
        }

        [NodePropertyGet(typeof(object), 4)]
        public object GetIndex(BlueprintInput data)
        {
            return buffer;
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
            if (fields[3].fieldType != ft)
            {
                fields[3] = new Field(ft, "Target", ConnectionType.DataInput, true, this, FieldContainer.Object);
            }
            if (fields[4].fieldType != ft)
            {
                fields[4] = new Field(ft, "Array", ConnectionType.DataBoth, true, this, FieldContainer.Array);
            }
        }
    }
}



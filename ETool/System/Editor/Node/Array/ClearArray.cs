using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Clear")]
    public class ClearArray : NodeBase
    {
        public ClearArray(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Clear Element";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Array", ConnectionType.DataOutput, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(object[]), 2)]
        public object[] GetIndex(BlueprintInput data)
        {
            return new object[0];
        }
    }
}


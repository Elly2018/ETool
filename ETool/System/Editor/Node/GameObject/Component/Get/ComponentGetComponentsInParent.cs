using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Component/Get/GetComponentsInParent")]
    public class ComponentGetComponentsInParent : NodeBase
    {
        public ComponentGetComponentsInParent(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Components In Parent";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Component, "Component", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, true, this, FieldContainer.Object));
        }

        private void UpdateField()
        {
            int fieldIndex = (int)Field.GetObjectByFieldType(FieldType.Component, fields[0].target);
            if (fields.Count == 2)
            {
                fields.Add(new Field((FieldType)fieldIndex, "Instance", ConnectionType.DataOutput, true, this, FieldContainer.Array));
            }
            if (fields.Count == 3)
            {
                if ((int)fields[2].fieldType != fieldIndex)
                {
                    fields[2] = new Field((FieldType)fieldIndex, "Instance", ConnectionType.DataOutput, true, this, FieldContainer.Array);
                }
            }
        }

        [NodePropertyGet(typeof(Component), 2)]
        public object[] GetTargetComponent(BlueprintInput data)
        {
            int fieldIndex = (int)Field.GetObjectByFieldType(FieldType.Component, fields[0].target);
            GameObject go = (GameObject)GetFieldOrLastInputField(1, data);
            if (go != null)
                return go.GetComponentsInParent(Field.GetTypeByFieldType((FieldType)fieldIndex));
            else
                return null;
        }

        public override void FieldUpdate()
        {
            UpdateField();
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            UpdateField();
        }
    }
}

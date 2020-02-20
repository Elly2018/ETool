using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Component/Get/GetComponentInChildren")]
    [Component_Menu]
    public class ComponentGetComponentInChildren : NodeBase
    {
        public ComponentGetComponentInChildren(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Component In Children";
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
                fields.Add(new Field((FieldType)fieldIndex, "Instance", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            }
            if (fields.Count == 3)
            {
                if ((int)fields[2].fieldType != fieldIndex)
                {
                    fields[2] = new Field((FieldType)fieldIndex, "Instance", ConnectionType.DataOutput, true, this, FieldContainer.Object);
                }
            }
        }

        [NodePropertyGet(typeof(Component), 2)]
        public object GetTargetComponent(BlueprintInput data)
        {
            int fieldIndex = (int)Field.GetObjectByFieldType(FieldType.Component, fields[0].target);
            GameObject go = (GameObject)GetFieldOrLastInputField(1, data);
            if (go != null)
                return go.GetComponentInChildren(Field.GetTypeByFieldType((FieldType)fieldIndex));
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

using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Component/Method/CheckComponentExist")]
    [Component_Menu]
    public class ComponentCheckComponentExist : NodeBase
    {
        public ComponentCheckComponentExist(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Check Component Exist";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Component, "Component", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Exist", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 2)]
        public bool GetTargetComponent(BlueprintInput data)
        {
            int fieldIndex = (int)Field.GetObjectByFieldType(FieldType.Component, fields[0].target);
            GameObject go = (GameObject)GetFieldOrLastInputField(1, data);
            if (go != null)
                return go.GetComponent(Field.GetTypeByFieldType((FieldType)fieldIndex)) != null;
            else
                return false;

        }
    }
}


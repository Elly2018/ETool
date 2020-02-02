using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Component/Method/AddComponent")]
    public class AddComponent : NodeBase
    {
        public AddComponent(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Add Component";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GameObject g = GetFieldOrLastInputField<GameObject>(2, data);
            FieldType c = GetFieldOrLastInputField<FieldType>(1, data);

            if(g != null)
            {
                g.AddComponent(Field.GetTypeByFieldType(c));
            }

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Component, "Component", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, true, this, FieldContainer.Object));
        }
    }
}


using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Get Active")]
    public class GetActive : NodeBase
    {
        public GetActive(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Active";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Active", ConnectionType.DataOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetGameObject(BlueprintInput data)
        {
            return ((GameObject)GetFieldOrLastInputField(1, data)).activeInHierarchy;
        }
    }
}



using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Component/Get Component")]
    public class GetComponent : NodeBase
    {
        public GetComponent(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Component";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Component, "Component", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, true, this, FieldContainer.Object));
        }
    }
}

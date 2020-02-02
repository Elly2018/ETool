using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/PC/GetAnyKey")]
    public class InputAnyKey : NodeBase
    {
        public InputAnyKey(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get AnyKey";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 1)]
        public bool GetResult(BlueprintInput data)
        {
            return Input.anyKey;
        }
    }
}


using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Get KeyUp")]
    public class KeyUp : NodeBase
    {
        public KeyUp(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "KeyUp";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Key, "Key", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 1)]
        public bool GetResult(BlueprintInput data)
        {
            return Input.GetKeyUp((KeyCode)fields[0].target.target_Int);
        }
    }
}

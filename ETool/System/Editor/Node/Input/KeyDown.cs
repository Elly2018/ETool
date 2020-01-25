﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Get KeyDown")]
    public class KeyDown : NodeBase
    {
        public KeyDown(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "KeyDown";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Key, "Key", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 1)]
        public bool GetResult(BlueprintInput data)
        {
            Debug.Log(Input.GetKeyDown((KeyCode)fields[0].target.target_Int));
            return Input.GetKeyDown((KeyCode)fields[0].target.target_Int);
        }
    }
}
﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/PC/GetKeyDown")]
    [Input_Menu("Keyborad")]
    public class InputKeyDown : NodeBase
    {
        public InputKeyDown(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "KeyDown";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Key, "Key", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 1)]
        public Boolean GetResult(BlueprintInput data)
        {
            return Input.GetKeyDown((KeyCode)Field.GetObjectByFieldType(FieldType.Key, fields[0].target));
        }
    }
}

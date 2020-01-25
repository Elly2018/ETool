﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Constant/String")]
    public class Constant_String : NodeBase
    {
        public Constant_String(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Constant String";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "String", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(String), 0)]
        public string GetString(BlueprintInput data)
        {
            return fields[0].target.target_String;
        }
    }
}

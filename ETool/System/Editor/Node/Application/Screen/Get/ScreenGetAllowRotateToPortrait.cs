﻿using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Get/GetAllowRotateToPortrait")]
    public class ScreenGetAllowRotateToPortrait : NodeBase
    {
        public ScreenGetAllowRotateToPortrait(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Allow Rotate To Portrait";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetID(BlueprintInput data)
        {
            return Screen.autorotateToPortrait;
        }
    }
}

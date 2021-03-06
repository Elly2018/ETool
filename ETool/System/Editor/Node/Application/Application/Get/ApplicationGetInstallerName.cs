﻿using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Application/Get/GetInstallerName")]
    public class ApplicationGetInstallerName : NodeBase
    {
        public ApplicationGetInstallerName(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Installer Name";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetID(BlueprintInput data)
        {
            return Application.installerName;
        }
    }
}

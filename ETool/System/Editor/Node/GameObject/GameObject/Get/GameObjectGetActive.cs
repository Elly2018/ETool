using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Get/GetActive")]
    [Transform_Menu("GameObject")]
    public class GameObjectGetActive : NodeBase
    {
        public GameObjectGetActive(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Active";
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



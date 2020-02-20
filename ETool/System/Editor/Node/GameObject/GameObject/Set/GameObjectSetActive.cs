using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Set/SetActive")]
    [Transform_Menu("GameObject")]
    public class GameObjectSetActive : NodeBase
    {
        public GameObjectSetActive(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Active";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ((GameObject)GetFieldOrLastInputField(2, data)).SetActive((bool)GetFieldOrLastInputField(1, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Active", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 2)]
        public GameObject GetGameObject(BlueprintInput data)
        {
            return (GameObject)GetFieldOrLastInputField(1, data);
        }
    }
}



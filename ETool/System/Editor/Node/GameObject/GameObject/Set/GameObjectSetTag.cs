using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Set/SetTag")]
    [Transform_Menu("GameObject")]
    public class GameObjectSetTag : NodeBase
    {
        public GameObjectSetTag(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Tag";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ((GameObject)GetFieldOrLastInputField(1, data)).tag = (string)GetFieldOrLastInputField(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Tag", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 1)]
        public GameObject GetGameObject(BlueprintInput data)
        {
            return (GameObject)GetFieldOrLastInputField(1, data);
        }
    }
}



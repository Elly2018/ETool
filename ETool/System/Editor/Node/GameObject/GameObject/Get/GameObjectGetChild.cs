using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Get/GetChild")]
    [Transform_Menu("GameObjectChild")]
    public class GameObjectGetChild : NodeBase
    {
        public GameObjectGetChild(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Child";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Index", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 2)]
        public GameObject GameAllChild(BlueprintInput data)
        {
            GameObject go = (GameObject)GetFieldOrLastInputField(1, data);
            return go.transform.GetChild((Int32)Field.GetObjectByFieldType(FieldType.Int, fields[0].target)).gameObject;
        }
    }
}



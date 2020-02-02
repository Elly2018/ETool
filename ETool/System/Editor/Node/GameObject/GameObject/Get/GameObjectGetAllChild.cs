using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Get/GetAllChild")]
    public class GameObjectGetAllChild : NodeBase
    {
        public GameObjectGetAllChild(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get All Child";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(GameObject[]), 1)]
        public GameObject[] GameAllChild(BlueprintInput data)
        {
            GameObject go = (GameObject)GetFieldOrLastInputField(0, data);
            List<GameObject> result = new List<GameObject>();
            for(int i = 0; i < go.transform.childCount; i++)
            {
                result.Add(go.transform.GetChild(i).gameObject);
            }
            return result.ToArray();
        }
    }
}



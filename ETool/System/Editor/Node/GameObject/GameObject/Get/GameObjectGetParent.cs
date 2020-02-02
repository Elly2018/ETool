using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Get/GetParent")]
    public class GameObjectGetParent : NodeBase
    {
        public GameObjectGetParent(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Parent";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 2)]
        public GameObject GameAllChild(BlueprintInput data)
        {
            GameObject go = (GameObject)GetFieldOrLastInputField(0, data);

            if (go != null)
                return go.transform.parent.gameObject;
            else
                return null;
        }
    }
}




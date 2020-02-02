using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Get/GetSiblingIndex")]
    public class GameObjectGetSiblingIndex : NodeBase
    {
        public GameObjectGetSiblingIndex(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Sibling Index";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Index", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 2)]
        public int GameAllChild(BlueprintInput data)
        {
            GameObject go = (GameObject)GetFieldOrLastInputField(0, data);

            if (go != null)
                return go.transform.GetSiblingIndex();
            else
                return -1;
        }
    }
}




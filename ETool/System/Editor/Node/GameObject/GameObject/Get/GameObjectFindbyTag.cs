using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Get/FindByTag")]
    public class GameObjectFindbyTag : NodeBase
    {
        public GameObjectFindbyTag(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Find By Tag";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Find", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 1)]
        public GameObject GetGameObject(BlueprintInput data)
        {
            return GameObject.FindGameObjectWithTag((string)GetFieldOrLastInputField(0, data));
        }
    }
}


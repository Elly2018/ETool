using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Find By Tag")]
    public class FindbyTag : NodeBase
    {
        public FindbyTag(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "GameObject Find By Tag";
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


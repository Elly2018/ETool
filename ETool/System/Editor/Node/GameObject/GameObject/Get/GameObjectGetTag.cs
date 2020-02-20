using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Get/GetTag")]
    [Transform_Menu("GameObject")]
    public class GameObjectGetTag : NodeBase
    {
        public GameObjectGetTag(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Tag";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(String), 1)]
        public string GameAllChild(BlueprintInput data)
        {
            GameObject go = (GameObject)GetFieldOrLastInputField(0, data);
            return go.tag;
        }
    }
}




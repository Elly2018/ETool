using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Get/SelfGameObject")]
    [Self_Menu]
    public class GameObjectSelfGameObject : NodeBase
    {
        public GameObjectSelfGameObject(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Self GameObject";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 0)]
        public GameObject GetGameObject(BlueprintInput data)
        {
            return data.thisGameobject;
        }
    }
}


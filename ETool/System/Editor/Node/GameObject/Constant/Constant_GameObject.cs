using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Constant/GameObject")]
    [Constant_Menu]
    public class Constant_GameObject : NodeBase
    {
        public Constant_GameObject(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant GameObject";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 0)]
        public GameObject GetGameObject(BlueprintInput data)
        {
            return (GameObject)Field.GetObjectByFieldType(FieldType.GameObject, fields[0].target);
        }
    }
}

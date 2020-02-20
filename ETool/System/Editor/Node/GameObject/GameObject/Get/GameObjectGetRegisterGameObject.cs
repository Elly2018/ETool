using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/GetRegister")]
    [Self_Menu]
    public class GameObjectGetRegisterGameObject : NodeBase
    {
        public GameObjectGetRegisterGameObject(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Register GameObject";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 1)]
        public GameObject GetRegister(BlueprintInput data)
        {
            foreach(var i in data.gameobjectRegister)
            {
                if (i.label == (string)GetFieldOrLastInputField(0, data)) return i.target;
            }
            Debug.LogWarning("Get register node can not find gameobject by this name:" + (string)GetFieldOrLastInputField(0, data));
            return null;
        }
    }
}

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Method/Create Empty")]
    public class GameObjectCreateEmptyGameObject : NodeBase
    {
        private GameObject buffer;

        public GameObjectCreateEmptyGameObject(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Create Empty";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            string name = GetFieldOrLastInputField<string>(1, data);
            Transform parent = GetFieldOrLastInputField<Transform>(2, data);
            GameObject g = new GameObject(name);
            g.transform.SetParent(parent);
            buffer = g;
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Parent", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 3)]
        public GameObject GetGO(BlueprintInput data)
        {
            return buffer;
        }
    }
}

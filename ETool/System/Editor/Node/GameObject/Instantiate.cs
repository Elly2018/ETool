using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Instantiate")]
    public class Instantiate : NodeBase
    {
        GameObject go;

        public Instantiate(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Instantiate";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GameObject v1 = (GameObject)GetFieldOrLastInputField(1, data);
            Transform v2 = (Transform)GetFieldOrLastInputField(2, data);
            if (v1 != null && v2 == null)
            {
                go = GameObject.Instantiate(v1);
            }
            else if (v1 != null && v2 != null)
            {
                go = GameObject.Instantiate(v1, v2);
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Parent", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 1)]
        public GameObject GetTarget(BlueprintInput data)
        {
            return go;
        }
    }
}


using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Set/SetParent")]
    [Transform_Menu("GameObjectChild")]
    public class SetParent : NodeBase
    {
        public SetParent(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Parent";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GameObject v1 = (GameObject)GetFieldOrLastInputField(1, data);
            GameObject v2 = (GameObject)GetFieldOrLastInputField(2, data);
            if(v1 != null)
            {
                if(v2 == null)
                {
                    v1.transform.SetParent(null);
                }
                else
                {
                    v1.transform.SetParent(v2.transform);
                }
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Parent To", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 1)]
        public GameObject GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<GameObject>(1, data);
        }
    }
}

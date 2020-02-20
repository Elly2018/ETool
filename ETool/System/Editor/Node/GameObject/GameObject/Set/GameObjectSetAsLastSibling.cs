using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Set/SetAsLastSibling")]
    [Transform_Menu("GameObjectChild")]
    public class GameObjectSetAsLastSibling : NodeBase
    {
        public GameObjectSetAsLastSibling(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set As Last Sibling";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GameObject v1 = (GameObject)GetFieldOrLastInputField(1, data);
            if (v1 != null)
            {
                v1.transform.SetAsLastSibling();
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(GameObject), 1)]
        public GameObject GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<GameObject>(1, data);
        }
    }
}

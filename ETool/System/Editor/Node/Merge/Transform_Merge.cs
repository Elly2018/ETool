using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Merge/Transform")]
    public class ATransform_Merge : NodeBase
    {
        private Transform local = null;

        public ATransform_Merge(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Transform";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            local = (Transform)GetFieldOrLastInputField(4, data);

            if(local != null)
            {
                local.transform.localPosition = (Vector3)GetFieldOrLastInputField(1, data);
                local.transform.rotation = (Quaternion)GetFieldOrLastInputField(2, data);
                local.transform.localScale = (Vector3)GetFieldOrLastInputField(3, data);
            }

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Local Position", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Quaternion, "Local Rotation", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Local Scale", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Result", ConnectionType.DataBoth, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Transform), 4)]
        public Transform GetTrans(BlueprintInput data)
        {
            return local;
        }
    }
}

using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Blueprint/GetByGameObject")]
    public class GetBPByGameObject : NodeBase
    {
        public GetBPByGameObject(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "BP Get By GameObject";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.GameObject, "Source", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(EBlueprint), 1)]
        public EBlueprint GetGameObject(BlueprintInput data)
        {
            return ((GameObject)GetFieldOrLastInputField(0, data)).GetComponent<NodeComponent>().ABlueprint;
        }
    }
}



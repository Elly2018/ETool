using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Blueprint/Get/GetBlueprintEqual")]
    [ETool_Menu("Blueprint")]
    public class BPGetEqual : NodeBase
    {
        public BPGetEqual(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Blueprint Equal";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "Try Casting", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.NodeComponent, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetBP(BlueprintInput data)
        {
            return GetFieldOrLastInputField<ENodeComponent>(2, data).GetInstanceOfBP() == GetFieldOrLastInputField<EBlueprint>(1, data);
        }
    }
}

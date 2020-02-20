using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Blueprint/Get/GetBlueprint")]
    [ETool_Menu("Blueprint")]
    public class BPGetBlueprintInstance : NodeBase
    {
        public BPGetBlueprintInstance(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Blueprint Instance";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Blueprint, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.NodeComponent, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(EBlueprint), 0)]
        public EBlueprint GetBP(BlueprintInput data)
        {
            return GetFieldOrLastInputField<ENodeComponent>(1, data).GetInstanceOfBP();
        }
    }
}

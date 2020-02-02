using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Get/GetCurrentAnimatorClipInfoCount")]
    public class GetCurrentAnimatorClipInfoCount : NodeBase
    {
        public GetCurrentAnimatorClipInfoCount(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Current Animator Clip Info Count";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Layer Index", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 2)]
        public int GetResult(BlueprintInput data)
        {
            int v1 = GetFieldOrLastInputField<int>(0, data);
            Animator v2 = GetFieldOrLastInputField<Animator>(1, data);
            if(v2 != null)
            {
                return v2.GetCurrentAnimatorClipInfoCount(v1);
            }
            return -1;
        }
    }
}

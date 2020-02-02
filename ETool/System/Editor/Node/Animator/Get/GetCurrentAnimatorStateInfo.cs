using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Get/GetCurrentAnimatorStateInfo")]
    public class GetCurrentAnimatorStateInfo : NodeBase
    {
        public GetCurrentAnimatorStateInfo(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Current Animator State Info";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Layer Index", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AnimatorStateInfo, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(AnimatorStateInfo), 2)]
        public AnimatorStateInfo? GetAnimatorClipInfos(BlueprintInput data)
        {
            int v1 = GetFieldOrLastInputField<int>(0, data);
            Animator v2 = GetFieldOrLastInputField<Animator>(1, data);

            if (v2 != null)
            {
                return v2.GetCurrentAnimatorStateInfo(v1);
            }
            return null;
        }
    }
}

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Get/GetCurrentAnimatorClipInfo")]
    public class GetCurrentAnimatorClipInfo : NodeBase
    {
        public GetCurrentAnimatorClipInfo(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Current Animator Clip Info";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Layer Index", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AnimatorClipInfo, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(AnimatorClipInfo[]), 2)]
        public AnimatorClipInfo[] GetAnimatorClipInfos(BlueprintInput data)
        {
            int v1 = GetFieldOrLastInputField<int>(0, data);
            Animator v2 = GetFieldOrLastInputField<Animator>(1, data);
            
            if(v2 != null)
            {
                return v2.GetCurrentAnimatorClipInfo(v1);
            }
            return null;
        }
    }
}

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Application/Get/IsFocused")]
    public class ApplicationIsFocused : NodeBase
    {
        public ApplicationIsFocused(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Is Focused";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetID(BlueprintInput data)
        {
            return Application.isFocused;
        }
    }
}

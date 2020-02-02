using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Mobile/GetTouch")]
    public class InputGetTouch : NodeBase
    {
        public InputGetTouch(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Touch";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Index", ConnectionType.DataInput, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.Touch, "Touch", ConnectionType.DataOutput, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(Touch), 1)]
        public Touch GetTouches(BlueprintInput data)
        {
            return Input.GetTouch(GetFieldOrLastInputField<int>(0, data));
        }
    }
}

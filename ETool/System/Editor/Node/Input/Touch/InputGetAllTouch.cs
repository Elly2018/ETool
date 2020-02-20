using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Mobile/GetAllTouch")]
    [Input_Menu("Touch")]
    public class InputGetAllTouch : NodeBase
    {
        public InputGetAllTouch(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get All Touches";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Touch, "All Touches", ConnectionType.DataOutput, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(Touch[]), 0)]
        public Touch[] GetTouches(BlueprintInput data)
        {
            return Input.touches;
        }
    }
}

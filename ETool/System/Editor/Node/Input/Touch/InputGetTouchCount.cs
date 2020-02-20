using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Mobile/GetTouchCount")]
    [Input_Menu("Touch")]
    public class InputGetTouchCount : NodeBase
    {
        public InputGetTouchCount(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Touch Count";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Count", ConnectionType.DataOutput, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetTouches(BlueprintInput data)
        {
            return Input.touchCount;
        }
    }
}

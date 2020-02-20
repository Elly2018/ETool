using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/PC/GetMousePosition")]
    [Input_Menu("Mouse")]
    public class InputMousePosition : NodeBase
    {
        public InputMousePosition(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Mouse Position";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector2, "Mouse Position", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector2), 0)]
        public Vector2 GetMousePos(BlueprintInput data)
        {
            return Input.mousePosition;
        }
    }
}

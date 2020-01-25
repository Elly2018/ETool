using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Get Mouse Position")]
    public class MousePosition : NodeBase
    {
        public MousePosition(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Mouse Position";
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

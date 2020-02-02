using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Mobile/GetTouchSupported")]
    public class InputGetTouchSupported : NodeBase
    {
        public InputGetTouchSupported(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Touch Supported";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Is Supported", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetTouches(BlueprintInput data)
        {
            return Input.touchSupported;
        }
    }
}

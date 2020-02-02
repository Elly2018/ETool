using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Mobile/GetTouchPressureSupported")]
    public class InputGetTouchPressureSupported : NodeBase
    {
        public InputGetTouchPressureSupported(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Touch Pressure Supported";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Is Supported", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetTouches(BlueprintInput data)
        {
            return Input.touchPressureSupported;
        }
    }
}


using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Get/GetFullScreenMode")]
    public class ScreenGetFullScreenMode : NodeBase
    {
        public ScreenGetFullScreenMode(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Full ScreenMode";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.FullScreenMode, "Mode", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(FullScreenMode), 0)]
        public FullScreenMode GetID(BlueprintInput data)
        {
            return Screen.fullScreenMode;
        }
    }
}

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Get/GetOrientation")]
    public class ScreenGetOrientation : NodeBase
    {
        public ScreenGetOrientation(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Orientation";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.ScreenOrientation, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(ScreenOrientation), 0)]
        public ScreenOrientation GetID(BlueprintInput data)
        {
            return Screen.orientation;
        }
    }
}

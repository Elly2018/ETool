using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Get/GetBrightness")]
    public class ScreenGetBrightness : NodeBase
    {
        public ScreenGetBrightness(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Brightness";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetID(BlueprintInput data)
        {
            return Screen.brightness;
        }
    }
}

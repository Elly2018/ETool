using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Set/SetBrightness")]
    public class ScreenSetBrightness : NodeBase
    {
        public ScreenSetBrightness(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Brightness";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            float v = GetFieldOrLastInputField<float>(1, data);
            Screen.brightness = v;
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

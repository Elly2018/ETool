using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Set/SetOrientation")]
    public class ScreenSetOrientation : NodeBase
    {
        public ScreenSetOrientation(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Orientation";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ScreenOrientation v = GetFieldOrLastInputField<ScreenOrientation>(1, data);
            Screen.orientation = v;
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.ScreenOrientation, "Orientation", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

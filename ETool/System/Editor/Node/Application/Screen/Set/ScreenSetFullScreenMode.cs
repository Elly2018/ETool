using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Set/SetFullScreenMode")]
    public class ScreenSetFullScreenMode : NodeBase
    {
        public ScreenSetFullScreenMode(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set FullScreen Mode";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            FullScreenMode v = GetFieldOrLastInputField<FullScreenMode>(1, data);
            Screen.fullScreenMode = v;
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.FullScreenMode, "Mode", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

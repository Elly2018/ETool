using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Set/SetAllowRotateToPortraitUpsideDown")]
    public class ScreenSetAllowRotateToPortraitUpsideDown : NodeBase
    {
        public ScreenSetAllowRotateToPortraitUpsideDown(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Allow Rotate To Portrait UpsideDown";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            bool v = GetFieldOrLastInputField<bool>(1, data);
            Screen.autorotateToPortraitUpsideDown = v;
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

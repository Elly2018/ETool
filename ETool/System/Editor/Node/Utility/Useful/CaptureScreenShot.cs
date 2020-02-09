using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Utility/Useful/CaptureScreenShot")]
    public class CaptureScreenShot : NodeBase
    {
        public CaptureScreenShot(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Capture ScreenShot";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ScreenCapture.CaptureScreenshot(GetFieldOrLastInputField<string>(1, data));
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "URL", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override StyleType GetNodeStyle()
        {
            return StyleType.Event_Node;
        }

        public override StyleType GetNodeSelectStyle()
        {
            return StyleType.Select_Event_Node;
        }
    }
}

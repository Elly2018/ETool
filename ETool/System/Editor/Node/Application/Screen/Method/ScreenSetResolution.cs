using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Method/SetResolution")]
    public class ScreenSetResolution : NodeBase
    {
        public ScreenSetResolution(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Resolution";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            int w = GetFieldOrLastInputField<int>(1, data);
            int h = GetFieldOrLastInputField<int>(2, data);
            FullScreenMode f = GetFieldOrLastInputField<FullScreenMode>(3, data);
            int r = GetFieldOrLastInputField<int>(4, data);

            Screen.SetResolution(w, h, f, r);

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Width", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Height", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.FullScreenMode, "Height", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "RefreshRate", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

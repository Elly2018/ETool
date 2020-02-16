using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Get/GetCurrentResolution")]
    public class ScreenGetCurrentResolution : NodeBase
    {
        public ScreenGetCurrentResolution(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Current Resolution";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Width", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Height", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "RefreshRate", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetID0(BlueprintInput data)
        {
            return Screen.currentResolution.width;
        }

        [NodePropertyGet(typeof(int), 1)]
        public int GetID1(BlueprintInput data)
        {
            return Screen.currentResolution.height;
        }

        [NodePropertyGet(typeof(int), 3)]
        public int GetID2(BlueprintInput data)
        {
            return Screen.currentResolution.refreshRate;
        }
    }
}

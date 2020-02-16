using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Get/GetWidth")]
    public class ScreenGetWidth : NodeBase
    {
        public ScreenGetWidth(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Width";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Width", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetID(BlueprintInput data)
        {
            return Screen.width;
        }
    }
}

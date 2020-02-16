using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Get/GetHeight")]
    public class ScreenGetHeight : NodeBase
    {
        public ScreenGetHeight(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Height";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Height", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetID(BlueprintInput data)
        {
            return Screen.height;
        }
    }
}

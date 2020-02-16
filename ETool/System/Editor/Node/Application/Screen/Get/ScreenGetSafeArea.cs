using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Get/GetSafeArea")]
    public class ScreenGetSafeArea : NodeBase
    {
        public ScreenGetSafeArea(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Safe Area";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Rect, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Rect), 0)]
        public Rect GetID(BlueprintInput data)
        {
            return Screen.safeArea;
        }
    }
}

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Screen/Get/GetDPI")]
    public class ScreenGetDPI : NodeBase
    {
        public ScreenGetDPI(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get DPI";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "DPI", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetID(BlueprintInput data)
        {
            return Screen.dpi;
        }
    }
}

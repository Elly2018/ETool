using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/RandomRange")]
    [Math_Menu("Random")]
    public class RandomRange : NodeBase
    {
        public RandomRange(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Random Range";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Min", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Max", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Random.Range((float)GetFieldOrLastInputField(0, data), (float)GetFieldOrLastInputField(1, data));
        }
    }
}


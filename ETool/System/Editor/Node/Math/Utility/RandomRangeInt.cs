using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/RandomRangeInt")]
    [Math_Menu("Random")]
    public class RandomRangeInt : NodeBase
    {
        public RandomRangeInt(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Random Range Int";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Min", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Max", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetFloat(BlueprintInput data)
        {
            return Random.Range((int)GetFieldOrLastInputField(0, data), (int)GetFieldOrLastInputField(1, data));
        }
    }
}


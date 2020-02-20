using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Utility/RandomSeed")]
    [Math_Menu("Random")]
    public class RandomSeed : NodeBase
    {
        public RandomSeed(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Random Range";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Speed", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Random.InitState(GetFieldOrLastInputField<int>(1, data));
            ActiveNextEvent(0, data);
        }
    }
}


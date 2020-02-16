using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Application/Get/IsPlaying")]
    public class ApplicationIsPlaying : NodeBase
    {
        public ApplicationIsPlaying(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Is Playing";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetID(BlueprintInput data)
        {
            return Application.isPlaying;
        }
    }
}

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Application/Get/GetPlatform")]
    public class ApplicationGetPlatform : NodeBase
    {
        public ApplicationGetPlatform(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Platform";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Platform, "Platform", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public RuntimePlatform GetID(BlueprintInput data)
        {
            return Application.platform;
        }
    }
}

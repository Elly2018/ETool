using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Application/Get/GetVersion")]
    public class ApplicationGetVersion : NodeBase
    {
        public ApplicationGetVersion(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Version";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "Version", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetID(BlueprintInput data)
        {
            return Application.version;
        }
    }
}

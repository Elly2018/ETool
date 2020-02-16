using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Application/Get/GetCloudProjectId")]
    public class ApplicationGetCloudProjectId : NodeBase
    {
        public ApplicationGetCloudProjectId(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Cloud Project Id";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "ID", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetID(BlueprintInput data)
        {
            return Application.cloudProjectId;
        }
    }
}

using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Application/Get/GetBuildGUID")]
    public class ApplicationGetBuildGUID : NodeBase
    {
        public ApplicationGetBuildGUID(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Build GUID";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "GUID", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public string GetID(BlueprintInput data)
        {
            return Application.buildGUID;
        }
    }
}

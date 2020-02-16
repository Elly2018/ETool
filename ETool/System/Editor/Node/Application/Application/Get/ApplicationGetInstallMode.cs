using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Application/Get/GetInstallMode")]
    public class ApplicationGetInstallMode : NodeBase
    {
        public ApplicationGetInstallMode(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Install Mode";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.InstallMode, "Mode", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(string), 0)]
        public ApplicationInstallMode GetID(BlueprintInput data)
        {
            return Application.installMode;
        }
    }
}

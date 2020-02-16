using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Application/Application/Get/IsEditor")]
    public class ApplicationIsEditor : NodeBase
    {
        public ApplicationIsEditor(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Is Editor";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetID(BlueprintInput data)
        {
            return Application.isEditor;
        }
    }
}

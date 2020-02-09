using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Constant/Flare")]
    public class Constant_Flare : NodeBase
    {
        public Constant_Flare(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Flare";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Flare, "Flare", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Flare), 0)]
        public Flare GetVector4(BlueprintInput data)
        {
            return (Flare)Field.GetObjectByFieldType(FieldType.Flare, fields[0].target);
        }
    }
}


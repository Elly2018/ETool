using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Constant/Material")]
    [Constant_Menu]
    public class Constant_Material : NodeBase
    {
        public Constant_Material(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Material";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Material, "Material", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Material), 0)]
        public Material GetVector4(BlueprintInput data)
        {
            return (Material)Field.GetObjectByFieldType(FieldType.Material, fields[0].target);
        }
    }
}


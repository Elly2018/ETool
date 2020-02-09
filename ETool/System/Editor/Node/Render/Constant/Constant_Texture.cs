using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Constant/Texture")]
    public class Constant_Texture : NodeBase
    {
        public Constant_Texture(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Texture";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Texture, "Texture", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Texture), 0)]
        public Texture GetVector4(BlueprintInput data)
        {
            return (Texture)Field.GetObjectByFieldType(FieldType.Texture, fields[0].target);
        }
    }
}


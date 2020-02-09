using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Constant/Texture2D")]
    public class Constant_Texture2D : NodeBase
    {
        public Constant_Texture2D(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Texture2D";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Texture2D, "Texture2D", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Texture2D), 0)]
        public Texture2D GetVector4(BlueprintInput data)
        {
            return (Texture2D)Field.GetObjectByFieldType(FieldType.Texture2D, fields[0].target);
        }
    }
}


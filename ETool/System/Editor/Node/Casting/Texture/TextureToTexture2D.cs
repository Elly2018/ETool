using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Texture/=>Texture2D")]
    public class TextureToTexture2D : NodeBase
    {
        public TextureToTexture2D(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Texture => Texture2D";
        }

        [NodePropertyGet(typeof(Texture2D), 0)]
        public Texture2D GetString(BlueprintInput data)
        {
            return ((Texture)GetFieldInputValue(0, data)) as Texture2D;
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Texture, "Type", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Texture2D, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

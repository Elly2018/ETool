using System;
using UnityEngine;
using UnityEngine.Video;

namespace ETool.ANode
{
    [NodePath("Add Node/Render/Constant/Clip")]
    [Constant_Menu]
    public class Constant_Clip : NodeBase
    {
        public Constant_Clip(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Constant Clip";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Clip, "Clip", ConnectionType.DataOutput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Flare), 0)]
        public VideoClip GetVector4(BlueprintInput data)
        {
            return (VideoClip)Field.GetObjectByFieldType(FieldType.Clip, fields[0].target);
        }
    }
}

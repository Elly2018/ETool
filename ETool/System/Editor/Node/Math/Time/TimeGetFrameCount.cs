using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Time/Frame Count")]
    public class TimeGetFrameCount : NodeBase
    {
        public TimeGetFrameCount(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Frame Count";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Value", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetFloat(BlueprintInput data)
        {
            return Time.frameCount;
        }
    }
}


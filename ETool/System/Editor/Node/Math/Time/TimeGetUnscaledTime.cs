using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Time/Unscaled Time")]
    public class TimeGetUnscaledTime : NodeBase
    {
        public TimeGetUnscaledTime(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Unscaled Time";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Time.unscaledTime;
        }
    }
}


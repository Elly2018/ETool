using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Time/Maximum Particle Delta Time")]
    [Math_Menu("Time")]
    public class TimeGetMaximumParticleDeltaTime : NodeBase
    {
        public TimeGetMaximumParticleDeltaTime(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Maximum Particle Delta Time";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public float GetFloat(BlueprintInput data)
        {
            return Time.maximumParticleDeltaTime;
        }
    }
}


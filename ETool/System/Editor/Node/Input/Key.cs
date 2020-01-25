using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Get Key")]
    public class GetKey : NodeBase
    {
        public GetKey(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Key";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Key, "Key", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 1)]
        public bool GetResult(BlueprintInput data)
        {
            return Input.GetKey((KeyCode)fields[0].target.target_Int);
        }
    }
}

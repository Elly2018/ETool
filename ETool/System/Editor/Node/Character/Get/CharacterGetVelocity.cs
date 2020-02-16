using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Character/Get/GetVelocity")]
    public class CharacterGetVelocity : NodeBase
    {
        public CharacterGetVelocity(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Character Velocity";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Character, "Target", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        public override void ConnectionUpdate()
        {
            NodeError nodeError = new NodeError() { errorType = NodeErrorType.ConnectionError, errorString = "The Target field must link a character controller" };
            bool gameObjectConnection = NodeBasedEditor.Instance.Check_ConnectionExist(this, 1, true);

            if (!gameObjectConnection)
            {
                AddNodeError(nodeError);
            }
            else
            {
                DeleteNodeError(nodeError);
            }
        }

        [NodePropertyGet(typeof(Vector3), 0)]
        public Vector3 GetID(BlueprintInput data)
        {
            return GetFieldOrLastInputField<CharacterController>(1, data).velocity;
        }
    }
}


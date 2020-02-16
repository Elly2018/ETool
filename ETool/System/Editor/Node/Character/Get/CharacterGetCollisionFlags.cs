using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Character/Get/GetCollisionFlags")]
    public class CharacterGetCollisionFlags : NodeBase
    {
        public CharacterGetCollisionFlags(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Character Collision Flags";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.CollisionFlag, "Flag", ConnectionType.DataOutput, true, this, FieldContainer.Object));
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

        [NodePropertyGet(typeof(CollisionFlags), 0)]
        public CollisionFlags GetID(BlueprintInput data)
        {
            return GetFieldOrLastInputField<CharacterController>(1, data).collisionFlags;
        }
    }
}


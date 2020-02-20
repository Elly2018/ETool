using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Character/Get/GetDetectCollisions")]
    public class CharacterGetDetectCollisions : NodeBase
    {
        public CharacterGetDetectCollisions(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Character Detect Collisions";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Character, "Target", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        public override void ConnectionUpdate()
        {
            NodeError nodeError = new NodeError() { errorType = NodeErrorType.ConnectionError, errorString = "The Target field must link a character controller" };
            bool gameObjectConnection = EBlueprint.GetBlueprintByNode(this).Check_ConnectionExist(this, 1, true);

            if (!gameObjectConnection)
            {
                AddNodeError(nodeError);
            }
            else
            {
                DeleteNodeError(nodeError);
            }
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetID(BlueprintInput data)
        {
            return GetFieldOrLastInputField<CharacterController>(1, data).detectCollisions;
        }
    }
}


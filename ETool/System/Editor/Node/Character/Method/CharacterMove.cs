using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Character/Method/Move")]
    public class CharacterMove : NodeBase
    {
        public CharacterMove(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Move Character";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            CharacterController cc = GetFieldOrLastInputField<CharacterController>(1, data);
            Vector3 motion = GetFieldOrLastInputField<Vector3>(2, data);
            cc.Move(motion);
            ActiveNextEvent(0, data);
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

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Character, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Motion", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

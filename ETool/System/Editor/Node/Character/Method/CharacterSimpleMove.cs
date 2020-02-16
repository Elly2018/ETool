using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Character/Method/SimpleMove")]
    public class CharacterSimpleMove : NodeBase
    {
        public CharacterSimpleMove(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Simple Move Character";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            CharacterController cc = GetFieldOrLastInputField<CharacterController>(1, data);
            Vector3 motion = GetFieldOrLastInputField<Vector3>(2, data);
            cc.SimpleMove(motion);
            ActiveNextEvent(0, data);
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

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Character, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Speed", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

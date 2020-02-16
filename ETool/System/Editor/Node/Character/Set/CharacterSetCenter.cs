using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Character/Set/SetCenter")]
    public class CharacterSetCenter : NodeBase
    {
        public CharacterSetCenter(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Character Center";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            CharacterController cc = GetFieldOrLastInputField<CharacterController>(1, data);
            Vector3 v = GetFieldOrLastInputField<Vector3>(2, data);
            if (cc != null)
                cc.center = v;
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

        [NodePropertyGet(typeof(CharacterController), 1)]
        public CharacterController GetID(BlueprintInput data)
        {
            return GetFieldOrLastInputField<CharacterController>(1, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Character, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Center", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

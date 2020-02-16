using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Character/Get/GetSkinWidth")]
    public class CharacterGetSkinWidth : NodeBase
    {
        public CharacterGetSkinWidth(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Character Skin Width";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
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

        [NodePropertyGet(typeof(float), 0)]
        public float GetID(BlueprintInput data)
        {
            return GetFieldOrLastInputField<CharacterController>(1, data).skinWidth;
        }
    }
}


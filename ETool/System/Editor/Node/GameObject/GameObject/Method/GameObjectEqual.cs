using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/GameObject/Get/Equal")]
    [Transform_Menu("GameObject")]
    public class GameObjectEqual : NodeBase
    {
        public GameObjectEqual(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "GameObject Equal";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Object A", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameObject, "Object B", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(bool), 0)]
        public bool GetGameObject(BlueprintInput data)
        {
            return GetFieldOrLastInputField<GameObject>(1, data) == GetFieldOrLastInputField<GameObject>(2, data);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ETool.ANode
{
    [NodePath("Add Node/SceneManager/Get/GetSceneAt")]
    public class SceneGetSceneAt : NodeBase
    {
        public SceneGetSceneAt(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Scene At";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Scene, "Scene", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Index", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Scene), 0)]
        public Scene GetCount(BlueprintInput data)
        {
            return SceneManager.GetSceneAt(GetFieldOrLastInputField<int>(1, data));
        }
    }
}

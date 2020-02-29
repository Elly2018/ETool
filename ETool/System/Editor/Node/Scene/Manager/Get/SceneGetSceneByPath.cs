using UnityEngine;
using UnityEngine.SceneManagement;

namespace ETool.ANode
{
    [NodePath("Add Node/SceneManager/Get/GetSceneByPath")]
    public class SceneGetSceneByPath : NodeBase
    {
        public SceneGetSceneByPath(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Scene By Path";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Scene, "Scene", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Scene), 0)]
        public Scene GetCount(BlueprintInput data)
        {
            return SceneManager.GetSceneByPath(GetFieldOrLastInputField<string>(1, data));
        }
    }
}

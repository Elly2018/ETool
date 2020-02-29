using UnityEngine;
using UnityEngine.SceneManagement;

namespace ETool.ANode
{
    [NodePath("Add Node/SceneManager/Get/GetSceneByBuildIndex")]
    public class SceneGetSceneByBuildIndex : NodeBase
    {
        public SceneGetSceneByBuildIndex(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Scene By BuildIndex";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Scene, "Scene", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Index", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Scene), 0)]
        public Scene GetCount(BlueprintInput data)
        {
            return SceneManager.GetSceneByBuildIndex(GetFieldOrLastInputField<int>(1, data));
        }
    }
}

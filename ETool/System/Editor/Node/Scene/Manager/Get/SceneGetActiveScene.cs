using UnityEngine;
using UnityEngine.SceneManagement;

namespace ETool.ANode
{
    [NodePath("Add Node/SceneManager/Get/GetSceneCount")]
    public class SceneGetActiveScene : NodeBase
    {
        public SceneGetActiveScene(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Active Scene";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Scene, "Scene", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Scene), 0)]
        public Scene GetCount(BlueprintInput data)
        {
            return SceneManager.GetActiveScene();
        }
    }
}

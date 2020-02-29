using UnityEngine;
using UnityEngine.SceneManagement;

namespace ETool.ANode
{
    [NodePath("Add Node/SceneManager/Get/GetSceneCountInBuildSetting")]
    public class SceneGetSceneCountInBuildSetting : NodeBase
    {
        public SceneGetSceneCountInBuildSetting(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Scene Count In BuildSetting";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Count", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetCount(BlueprintInput data)
        {
            return SceneManager.sceneCountInBuildSettings;
        }
    }
}

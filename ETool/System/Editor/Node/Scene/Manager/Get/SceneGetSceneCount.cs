using UnityEngine;
using UnityEngine.SceneManagement;

namespace ETool.ANode
{
    [NodePath("Add Node/SceneManager/Get/GetSceneCount")]
    public class SceneGetSceneCount : NodeBase
    {
        public SceneGetSceneCount(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Scene Count";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Count", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 0)]
        public int GetCount(BlueprintInput data)
        {
            return SceneManager.sceneCount;
        }
    }
}

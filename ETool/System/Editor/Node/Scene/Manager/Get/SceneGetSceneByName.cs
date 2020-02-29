using UnityEngine;
using UnityEngine.SceneManagement;

namespace ETool.ANode
{
    [NodePath("Add Node/SceneManager/Get/GetSceneByName")]
    public class SceneGetSceneByName : NodeBase
    {
        public SceneGetSceneByName(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Scene By Name";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Scene, "Scene", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Scene), 0)]
        public Scene GetCount(BlueprintInput data)
        {
            return SceneManager.GetSceneByName(GetFieldOrLastInputField<string>(1, data));
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ETool.ANode
{
    [NodePath("Add Node/SceneManager/Method/CreateScene")]
    public class SceneCreateScene : NodeBase
    {
        private Scene buffer;

        public SceneCreateScene(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Create Scene";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Scene, "Scene", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            buffer = SceneManager.CreateScene(GetFieldOrLastInputField<string>(1, data));
            base.ProcessCalling(data);
        }

        [NodePropertyGet(typeof(string), 1)]
        public string GetCount(BlueprintInput data)
        {
            return GetFieldOrLastInputField<string>(1, data);
        }

        [NodePropertyGet(typeof(Scene), 2)]
        public Scene GetScene(BlueprintInput data)
        {
            return buffer;
        }
    }
}

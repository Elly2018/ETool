using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameData/Save GameData")]
    public class SaveData : NodeBase
    {
        public SaveData(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Save GameData";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            base.ProcessCalling(data);
        }

        public override void FieldInitialize()
        {
            base.FieldInitialize();
        }
    }
}

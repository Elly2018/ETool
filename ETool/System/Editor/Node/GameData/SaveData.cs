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
            string path = GetFieldOrLastInputField<string>(1, data);
            EGameData gamedata = GetFieldOrLastInputField<EGameData>(2, data);
            bool usejson = GetFieldOrLastInputField<bool>(3, data);

            if(gamedata != null)
            {
                if (usejson)
                    gamedata.Save_Json(path);
                else
                    gamedata.Save_Json_Binary(path);
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameData, "Data", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Use Json", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

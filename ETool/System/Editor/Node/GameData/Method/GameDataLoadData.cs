using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameData/Method/LoadGameData")]
    public class GameDataLoadData : NodeBase
    {
        private EGameData gamedataBuffer;

        public GameDataLoadData(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Load GameData";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            string path = GetFieldOrLastInputField<string>(1, data);
            EGameData gamedata = GetFieldOrLastInputField<EGameData>(2, data);
            bool usejson = GetFieldOrLastInputField<bool>(3, data);

            if (gamedata != null)
            {
                if (usejson)
                    gamedata.Load_Json(path);
                else
                    gamedata.Load_Json_Binary(path);
                gamedataBuffer = gamedata;
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Path", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.GameData, "Data", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Use Json", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(EGameData), 2)]
        public EGameData GetData(BlueprintInput data)
        {
            return gamedataBuffer;
        }
    }
}

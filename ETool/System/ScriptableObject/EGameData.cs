using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace ETool
{
    [CreateAssetMenu(menuName = "ETool/GameData")]
    public class EGameData : ScriptableObject
    {
        public GameDataStruct gameDataStruct;

        public void Save_Json(string path)
        {
            File.WriteAllText(path, JsonUtility.ToJson(gameDataStruct, true));
        }

        public void Save_Json_Binary(string path)
        {
            File.WriteAllBytes(path, Encoding.ASCII.GetBytes(JsonUtility.ToJson(gameDataStruct, true)));
        }

        public void Load_Json(string path)
        {
            gameDataStruct = JsonUtility.FromJson<GameDataStruct>(File.ReadAllText(path));
        }

        public void Load_Json_Binary(string path)
        {
            gameDataStruct = JsonUtility.FromJson<GameDataStruct>(Encoding.ASCII.GetString(File.ReadAllBytes(path)));
        }

        public void SetData(string category, string element, object o, FieldType type)
        {
            GameDataCategory c = GetCate(category, gameDataStruct.gameDataCategories);
            if (c == null)
            {
                Debug.LogWarning("Cannot data category find: " + category);
                return;
            }
            GameDataElement g = GetElement(element, c.gameDataElements);
            if (g == null)
            {
                Debug.LogWarning("Cannot data element find: " + element);
                return;
            }
            Field.SetObjectByFieldType(type, g.value, o);
        }

        public object GetData(string category, string element, FieldType type)
        {
            GameDataCategory c = GetCate(category, gameDataStruct.gameDataCategories);
            if (c == null)
            {
                Debug.LogWarning("Cannot data category find: " + category);
                return null;
            }
            GameDataElement g = GetElement(element, c.gameDataElements);
            if (g == null)
            {
                Debug.LogWarning("Cannot data element find: " + element);
                return null;
            }
            return Field.GetObjectByFieldType(type, g.value);
        }

        private GameDataCategory GetCate(string label, List<GameDataCategory> dataCategories)
        {
            foreach(var i in dataCategories)
            {
                if (i.label == label) return i;
            }
            return null;
        }

        private GameDataElement GetElement(string label, List<GameDataElement> GameDataElement)
        {
            foreach (var i in GameDataElement)
            {
                if (i.label == label) return i;
            }
            return null;
        }
    }
}

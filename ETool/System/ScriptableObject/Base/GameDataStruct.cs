using System.Collections.Generic;
using UnityEngine;

namespace ETool
{
    [System.Serializable]
    public class GameDataStruct
    {
        public List<GameDataCategory> gameDataCategories = new List<GameDataCategory>();
    }

    [System.Serializable]
    public class GameDataCategory
    {
        public string label = "New Category";
        public bool fold;
        public List<BlueprintVariable> gameDataElements = new List<BlueprintVariable>();
    }
}

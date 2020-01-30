using System.Collections.Generic;

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
        public List<GameDataElement> gameDataElements = new List<GameDataElement>();
    }

    [System.Serializable]
    public class GameDataElement
    {
        public string label;

        public FieldType fieldType;
        public GenericObject value = new GenericObject();
        public GenericObject[] value_array = new GenericObject[0];
    }
}

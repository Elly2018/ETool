using System.Collections.Generic;

namespace ETool
{
    /// <summary>
    /// ETool struct class
    /// </summary>
    [System.Serializable]
    public class StructBase : ObjectBase
    {
        public List<StructElement> structElement = new List<StructElement>();
    }

    /// <summary>
    /// ETool struct element
    /// It contain what element needs
    /// </summary>
    [System.Serializable]
    public class StructElement
    {
        /// <summary>
        /// Element label, for search use
        /// </summary>
        public string label;

        /// <summary>
        /// Element data type
        /// </summary>
        public FieldType type;

        /// <summary>
        /// 
        /// </summary>
        public GenericObject elementDefault;
        public GenericObject[] elementDefaultArray;
        public int structDataType;
    }

    public enum StructDataType
    {
        Object = 0,
        Array = 1,
        KeyMap = 2,
    }
}

using System.Collections.Generic;

namespace ETool
{
    /// <summary>
    /// This is the list, like item manager
    /// </summary>
    [System.Serializable]
    public class ListBase : ObjectBase
    {
        public List<ListElement> listElements = new List<ListElement>();
    }

    [System.Serializable]
    public class ListElement
    {
        /// <summary>
        /// 
        /// </summary>
        public GenericObject target;
    }
}

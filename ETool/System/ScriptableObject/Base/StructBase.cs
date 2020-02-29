using System.Collections.Generic;

namespace ETool
{
    /// <summary>
    /// ETool struct class
    /// </summary>
    [System.Serializable]
    public class StructBase : ObjectBase
    {
        public List<BlueprintVariable> structElement = new List<BlueprintVariable>();
    }
}

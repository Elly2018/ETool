using UnityEngine;

namespace ETool
{
    public partial class EBlueprint : ScriptableObject
    {
        public static NodeBase GetNodeByField(Field t)
        {
            foreach(var i in EBlueprint.GetAllBlueprint)
            {
                foreach(var j in i.nodes)
                {
                    foreach(var k in j.fields)
                    {
                        if (k == t) return j;
                    }
                }
            }
            return null;
        }
    }
}

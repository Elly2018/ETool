using UnityEngine;

namespace ETool
{
    public class DebugHelper
    {
        public static void Log(string message, NodeBase nb)
        {
            string blueprintName = EBlueprint.GetBlueprintByNode(nb).name;
            string prefix = "[" + blueprintName + "." + nb.unlocalTitle + "" + EBlueprint.GetBlueprintNodeIndexByNode(nb) + "]";
            Debug.Log(prefix + " " + message);
        }
    }
}

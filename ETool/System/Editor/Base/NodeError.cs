using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETool
{
    public enum NodeErrorType
    {
        NoReturn,
        Varaible_Does_Not_Exist,
        Custom_Event_Does_Not_Exist,
    }

    [System.Serializable]
    public class NodeError
    {
        public NodeErrorType errorType;
        public string errorString;

        public static bool CheckNodehaveErrorMessage(NodeBase target, NodeErrorType type)
        {
            foreach(var i in target.nodeErrors)
            {
                if (i.errorType == type) return true;
            }
            return false;
        }
    }
}

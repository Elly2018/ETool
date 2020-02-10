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
        ConnectionError,
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

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(NodeError)) return false;
            return errorString == ((NodeError)obj).errorString && errorType == ((NodeError)obj).errorType;
        }

        public static bool operator !=(NodeError a1, NodeError a2)
        {
            return a1.errorString != a2.errorString || a1.errorType != a2.errorType;
        }

        public static bool operator==(NodeError a1, NodeError a2)
        {
            return a1.errorString == a2.errorString && a1.errorType == a2.errorType;
        }
    }
}

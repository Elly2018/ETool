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
        public string title;
        public string errorString;
        public int code;

        public static bool CheckNodehaveErrorMessage(NodeBase target, NodeErrorType type, int code)
        {
            foreach(var i in target.nodeErrors)
            {
                if (i.errorType == type && i.code == code) return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(NodeError)) return false;
            return errorString == ((NodeError)obj).errorString && errorType == ((NodeError)obj).errorType && code == ((NodeError)obj).code && title == ((NodeError)obj).title;
        }

        public static bool operator !=(NodeError a1, NodeError a2)
        {
            return a1.errorString != a2.errorString || a1.errorType != a2.errorType || a1.code != a2.code || a1.title != a2.title;
        }

        public static bool operator==(NodeError a1, NodeError a2)
        {
            return a1.errorString == a2.errorString && a1.errorType == a2.errorType && a1.code == a2.code && a1.title == a2.title;
        }
    }
}

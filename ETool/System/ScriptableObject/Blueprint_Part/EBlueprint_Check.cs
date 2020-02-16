using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool
{
    public partial class EBlueprint : ScriptableObject
    {
        public static EBlueprint GetBlueprintByNode(Node node)
        {
            foreach(var i in GetAllBlueprint)
            {
                foreach(var j in i.nodes)
                {
                    if (j == node) return i;
                }
            }
            return null;
        }

        /// <summary>
        /// Check if any other node are selected <br />
        /// This is use for multiple node drag
        /// </summary>
        /// <param name="node">Source node</param>
        /// <returns></returns>
        public bool Check_AnyOtherNodeAreSelected(Node node)
        {
            foreach (var i in nodes)
            {
                if (i != node && i.isSelected) return true;
            }
            return false;
        }

        /// <summary>
        /// Check if any other node is selected <br />
        /// This will effect the right click menu
        /// </summary>
        /// <returns></returns>
        public bool Check_AnyNodeSelect()
        {
            foreach (var i in nodes)
            {
                if (i.isSelected) return true;
            }
            return false;
        }

        /// <summary>
        /// Search in nodes list and check if target node exist in the list
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool Check_NodeExist(NodeBase n)
        {
            foreach (var i in nodes)
            {
                if (i == n) return true;
            }
            return false;
        }

        /// <summary>
        /// Check if any other connection is selected <br />
        /// This will effect the right click menu
        /// </summary>
        /// <returns></returns>
        public bool Check_AnyConnectionSelect()
        {
            foreach (var i in connections)
            {
                if (i.isSelected) return true;
            }
            return false;
        }

        /// <summary>
        /// Check the target field have connection link to it
        /// </summary>
        /// <param name="node">Target Node</param>
        /// <param name="field">Field Index</param>
        /// <param name="input">Input Or Output</param>
        /// <returns></returns>
        public bool Check_ConnectionExist(NodeBase node, int field, bool input)
        {
            int n = nodes.IndexOf(node);
            foreach (var i in connections)
            {
                if (input && i.inPointMark == new Vector2Int(n, field))
                {
                    return true;
                }
                if (!input && i.outPointMark == new Vector2Int(n, field))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check the target field have connection link to it
        /// </summary>
        /// <param name="node">Target Node</param>
        /// <param name="field">Field Index</param>
        /// <param name="input">Input Or Output</param>
        /// <returns></returns>
        public bool Check_ConnectionExist(int node, int field, bool input)
        {
            int n = node;
            foreach (var i in connections)
            {
                if (input && i.inPointMark == new Vector2Int(n, field))
                {
                    return true;
                }
                if (!input && i.outPointMark == new Vector2Int(n, field))
                {
                    return true;
                }
            }
            return false;
        }

        

        /// <summary>
        /// Check the input connection type match anything in the list
        /// </summary>
        /// <param name="_in">Input mark</param>
        /// <param name="_out">Output mark</param>
        /// <returns></returns>
        public bool Check_ConnectionExist(Vector2Int _in, Vector2 _out)
        {
            foreach (var i in connections)
            {
                if (i.inPointMark == _in && i.outPointMark == _out) return true;
            }
            return false;
        }

        /// <summary>
        /// For check event or method type node use
        /// </summary>
        /// <param name="event_node">Target Event Type</param>
        /// <returns></returns>
        public NodeBase[] Check_GetEventNode(EventNodeType event_node)
        {
            List<NodeBase> result = new List<NodeBase>();
            foreach (var i in nodes)
            {
                string nodename = "ETool.ANode.A" + event_node.ToString();

                if (i.GetType().FullName == nodename) result.Add(i);
            }
            return result.ToArray();
        }

        /// <summary>
        /// For check event or method type node use
        /// </summary>
        /// <param name="event_node">Target Event Type</param>
        /// <returns></returns>
        public bool Check_EventNodeExist(EventNodeType event_node)
        {
            foreach (var i in nodes)
            {
                string nodename = "ETool.ANode.A" + event_node.ToString();
                if (i.NodeType == nodename) return true;
            }
            return false;
        }
    }
}

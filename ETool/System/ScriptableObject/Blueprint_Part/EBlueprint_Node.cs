using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    public partial class EBlueprint : ScriptableObject
    {
        /// <summary>
        /// When the right click menu active <br />
        /// Create instance of target in this position on the grid
        /// </summary>
        /// <param name="mousePosition">Mouse pos</param>
        public NodeBase Node_AddNode(Vector2 mousePosition, int selectPage, Type addNode)
        {
            /* Default */
            List<object> _args = new List<object>();
            _args.Add(mousePosition);
            _args.Add(200);
            _args.Add(60);

            NodeBase n = (NodeBase)assembly.CreateInstance(addNode.FullName, false, BindingFlags.Public | BindingFlags.Instance, null, _args.ToArray(), null, null);
            nodes.Add(n);
            n.Initialize();
            n.PostFieldInitialize();
            n.page = selectPage;
            AssetDatabase.SaveAssets();
            return n;
        }

        /// <summary>
        /// Delete selection nodes
        /// </summary>
        public void Node_DeleteSelectionNode()
        {
            List<NodeBase> nb = new List<NodeBase>();
            foreach (var i in nodes)
            {
                if (i.isSelected) nb.Add(i);
            }

            for(int i = 0; i < nb.Count; i++)
            {
                if (Type.GetType(nb[i].NodeType).GetCustomAttribute<CanNotDelete>() == null)
                {
                    Node_RemoveNode(nb[i]);
                }
            }
        }

        /// <summary>
        /// This will only use in editor use
        /// </summary>
        /// <param name="nodes">Target node array</param>
        public void Node_RemoveNodes(NodeBase[] _nodes)
        {
            foreach (var i in _nodes) Node_RemoveNode(i);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// This will only use in editor use
        /// </summary>
        /// <param name="nodes">Target node array</param>
        public void Node_RemoveNode(NodeBase n_nodesodes)
        {
            List<Connection> clist = new List<Connection>();
            foreach(var i in connections)
            {
                if(i.inPointMark.x == nodes.IndexOf(n_nodesodes) || i.outPointMark.x == nodes.IndexOf(n_nodesodes))
                {
                    clist.Add(i);
                }
            }

            for(int i = 0; i < clist.Count; i++)
            {
                Connection_RemoveConnection(clist[i]);
            }

            foreach(var i in connections)
            {
                if (i.inPointMark.x > nodes.IndexOf(n_nodesodes))
                {
                    i.inPointMark.x--;
                }
                if (i.outPointMark.x > nodes.IndexOf(n_nodesodes))
                {
                    i.outPointMark.x--;
                }
            }

            nodes.Remove(n_nodesodes);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Clean all node selection
        /// </summary>
        public void Node_CleanNodeSelection()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].isSelected = false;
            }
        }

        /// <summary>
        /// Get the target field node parent
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public NodeBase Node_GetNodeByField(Field field)
        {
            foreach (var i in nodes)
            {
                foreach (var j in i.fields)
                {
                    if (j == field) return i;
                }
            }
            return null;
        }

        public Type[] Node_GetAllNodebaseType()
        {
            Type[] allTypes = assembly.GetTypes();
            List<ForNodeNameSort> search = new List<ForNodeNameSort>();
            foreach (var i in allTypes)
            {
                NodePath np = i.GetCustomAttribute<NodePath>();
                if (np != null && i.GetCustomAttribute<NodeHide>() == null)
                    search.Add(new ForNodeNameSort() { type = i, nodepath = np.Path });
            }

            /* Sort by type name */
            List<Type> sorted = new List<Type>();
            var order = from e in search orderby e.nodepath select e;
            foreach (var i in order)
            {
                sorted.Add(i.type);
            }
            allTypes = sorted.ToArray();
            return allTypes;
        }
    }
}

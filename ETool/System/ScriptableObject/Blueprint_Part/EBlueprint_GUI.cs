using ETool.ANode;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace ETool
{
    public partial class EBlueprint : ScriptableObject
    {
        /// <summary>
        /// When the right click menu active <br />
        /// And user is click add node <br />
        /// local variable: addnode will change to target
        /// </summary>
        /// <param name="addEvent">Target node (Strcut)AddClickEvent </param>
        public void GUI_OnClickAddNode(object addEvent)
        {
            AddClickEvent ace = (AddClickEvent)addEvent;
            NodeBase nb = Node_AddNode(ace.mousePosition, ace.page, ace.add);

            if (nb.NodeType == typeof(GetVariable).FullName)
            {
                (nb as GetVariable).SetOptions(GetInheritVariable());
            }

            if (nb.NodeType == typeof(SetVariable).FullName)
            {
                (nb as SetVariable).SetOptions(GetInheritVariable());
            }
        }

        public void GUI_OnClickAddCustomEvent(object addEvent)
        {
            AddCustomEvent ace = (AddCustomEvent)addEvent;
            NodeBase n = Node_AddNode(ace.mousePosition, ace.page, typeof(ACustomEventCall));
            ACustomEventCall nec = n as ACustomEventCall;
            if (nec != null)
            {
                nec.targetEventOrVar = ace.tbp.name + "." + ace.bce.eventName;
                nec.unlocalTitle = ace.bce.eventName;
                nec.SetCustomEvent(ace.tbp, ace.bce);
                nec.isInherit = ace.isInherit;
            }
        }

        public void GUI_OnClickAddReturnNode(object addEvent)
        {
            AddClickEvent Spawn = (AddClickEvent)addEvent;
            NodeBase nb = Node_AddNode(Spawn.mousePosition, Spawn.page, typeof(AReturn));
            nb.returnType = blueprintEvent.customEvent[Spawn.page - EBlueprint.DefaultPageCount].returnType;
            nb.returnContainer = blueprintEvent.customEvent[Spawn.page - EBlueprint.DefaultPageCount].returnContainer;
            (nb as AReturn).SetReturnType();
        }

        /// <summary>
        /// This will send as delegate to all node <br />
        /// When node need to be delete <br />
        /// this event will called
        /// </summary>
        /// <param name="node">Target node</param>
        public void GUI_OnClickRemoveNode(NodeBase node)
        {
            List<Connection> removeList = new List<Connection>();
            int index = nodes.IndexOf(node);
            /* Delete related connection */
            for (int i = 0; i < connections.Count; i++)
            {
                if (nodes.IndexOf(node) == connections[i].inPointMark.x)
                {
                    removeList.Add(connections[i]);
                }

                if (nodes.IndexOf(node) == connections[i].outPointMark.x)
                {
                    removeList.Add(connections[i]);
                }
            }

            /* Delete connection list */
            foreach (var i in removeList)
            {
                Connection_RemoveConnection(i);
            }

            /* Delete node from node list */
            nodes.Remove(node);

            /* Shift connection */
            foreach (var i in connections)
            {
                if (i.inPointMark.x > index)
                {
                    i.inPointMark.x--;
                }
                if (i.outPointMark.x > index)
                {
                    i.outPointMark.x--;
                }
            }
        }

        

    }
}

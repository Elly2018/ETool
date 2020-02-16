using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    public partial class EBlueprint : ScriptableObject
    {
        public string Connection_CreateConnection(ConnectionPoint cp_in, ConnectionPoint cp_out, int page)
        {
            Vector2Int _in = Connection_GetConnectionInfo(cp_in);
            Vector2Int _out = Connection_GetConnectionInfo(cp_out);

            /* Field type check */
            if (nodes[_in.x].fields[_in.y].fieldType != nodes[_out.x].fields[_out.y].fieldType)
            {
                return "Type mismatch";
            }

            if (nodes[_in.x].fields[_in.y].fieldContainer != nodes[_out.x].fields[_out.y].fieldContainer)
            {
                return "Container mismatch";
            }

            foreach (var i in connections)
            {
                if (i.inPointMark == _in)
                {
                    return "Twice input detect";
                }
            }

            Connection c = new Connection(_in, _out, false);
            c.page = page;

            /* Set field to connection */
            c.fieldType = nodes[_in.x].fields[_in.y].fieldType;

            /* Trigger on connection statt */
            nodes[_in.x].fields[_in.y].onConnection = true;

            connections.Add(c);

            nodes[_in.x].ConnectionUpdate();
            nodes[_out.x].ConnectionUpdate();

            GUI.changed = true;

            Connection_CleanConnectionPointSelection();

            AssetDatabase.SaveAssets();
            return null;
        }

        public void Connection_DeleteSelectedConnection()
        {
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].isSelected)
                {
                    Connection_RemoveConnection(connections[i]);
                    i--;
                }
            }
        }

        public void Connection_RemoveConnection(Connection connection)
        {
            int x = connection.inPointMark.x;
            int y = connection.outPointMark.y;
            nodes[connection.inPointMark.x].fields[connection.inPointMark.y].onConnection = false;
            connections.Remove(connection);

            nodes[x].ConnectionUpdate();
            nodes[y].ConnectionUpdate();

            GUI.changed = true;
            AssetDatabase.SaveAssets();
        }

        public void Connection_RemoveConnection(int connection)
        {
            Connection_RemoveConnection(connections[connection]);
        }

        public void Connection_RemoveRelateConnectionInField(Field field)
        {
            List<Connection> removeConnection = new List<Connection>();
            if (field.connectionType == ConnectionType.DataBoth || field.connectionType == ConnectionType.EventBoth)
            {
                Vector2Int t = Connection_GetConnectionInfo(field.inPoint);
                foreach (var i in connections)
                {
                    if (i.inPointMark == t) removeConnection.Add(i);
                }
                t = Connection_GetConnectionInfo(field.outPoint);
                foreach (var i in connections)
                {
                    if (i.outPointMark == t) removeConnection.Add(i);
                }
            }
            else if (field.connectionType == ConnectionType.DataInput || field.connectionType == ConnectionType.EventInput)
            {
                Vector2Int t = Connection_GetConnectionInfo(field.inPoint);
                foreach (var i in connections)
                {
                    if (i.inPointMark == t) removeConnection.Add(i);
                }
            }
            else if (field.connectionType == ConnectionType.DataOutput || field.connectionType == ConnectionType.EventOutput)
            {
                Vector2Int t = Connection_GetConnectionInfo(field.outPoint);
                foreach (var i in connections)
                {
                    if (i.outPointMark == t) removeConnection.Add(i);
                }
            }

            foreach (var i in removeConnection)
            {
                Connection_RemoveConnection(i);
            }
        }

        public Vector2Int Connection_GetConnectionInfo(ConnectionPoint c)
        {
            Vector2Int result = Vector2Int.zero;
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes[i].fields.Count; j++)
                {
                    if (nodes[i].fields[j].inPoint == c || nodes[i].fields[j].outPoint == c) return new Vector2Int(i, j);
                }
            }
            return result;
        }

        /// <summary>
        /// Clean all connection selection
        /// </summary>
        public void Connection_CleanConnectionSelection()
        {
            for (int i = 0; i < connections.Count; i++)
            {
                connections[i].isSelected = false;
            }
        }

        public ConnectionPoint Connection_GetConnectionPoint(Vector2Int mark, bool input)
        {
            if (input)
            {
                return nodes[mark.x].fields[mark.y].inPoint;
            }
            else
            {
                return nodes[mark.x].fields[mark.y].outPoint;
            }
        }

        public void Connection_CleanConnectionPointSelection()
        {
            foreach(var i in nodes)
            {
                foreach(var j in i.fields)
                {
                    j.inPoint.Selected = false;
                    j.outPoint.Selected = false;
                }
            }
            GUI.changed = true;
        }
    }
}

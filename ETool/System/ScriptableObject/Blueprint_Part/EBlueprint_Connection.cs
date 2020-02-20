using ETool.ANode;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    public partial class EBlueprint : ScriptableObject
    {
        // in, out, out_to_index, index_to_in, type
        private List<Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>> castingFeature = new List<Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>>()
        {
            /*
             * To String
            */
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.String, FieldType.Int, 0, 1, typeof(IntToString), null),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.String, FieldType.Float, 0, 1, typeof(FloatToString), null),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.String, FieldType.Boolean, 0, 1, typeof(BooleanToString), null),

            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.String, FieldType.Vector2, 0, 1, typeof(Vector2ToString), null),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.String, FieldType.Vector3, 0, 1, typeof(Vector3ToString), null),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.String, FieldType.Vector4, 0, 1, typeof(Vector4ToString), null),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.String, FieldType.Color, 0, 1, typeof(ColorToString), null),

            /*
             * Component
            */
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.Rigidbody, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.Rigidbody2D, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.Light, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.Camera, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.Collider, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.Collider2D, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.MeshFilter, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.MeshRenderer, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.Animator, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.NodeComponent, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.AudioSource, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.Character, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
            new Tuple<FieldType, FieldType, int, int, Type, Action<NodeBase, FieldType>>(FieldType.VideoPlayer, FieldType.GameObject, 1, 2, typeof(ComponentGetComponent), new Action<NodeBase, FieldType>(Connection_CastingAction_Component)),
        };

        public static void Connection_CastingAction_Component(NodeBase nb, FieldType type)
        {
            ComponentGetComponent cgc = nb as ComponentGetComponent;
            cgc.fields[0].target.genericBasicType.target_Int = (int)type;
            cgc.FieldUpdate();
        }

        public void Connection_FieldCastingFeature(ConnectionPoint cp_in, ConnectionPoint cp_out, int page)
        {
            Vector2Int _in = Connection_GetConnectionInfo(cp_in);
            Vector2Int _out = Connection_GetConnectionInfo(cp_out);

            FieldType t1 = nodes[_in.x].fields[_in.y].fieldType;
            FieldType t2 = nodes[_out.x].fields[_out.y].fieldType;

            foreach (var i in castingFeature)
            {
                if (i.Item1 == t1 && i.Item2 == t2)
                {
                    Vector2 pos = (nodes[_in.x].rect.position + nodes[_out.x].rect.position) / 2;
                    NodeBase nb = Node_AddNode(pos, page, i.Item5);
                    if(i.Item6 != null)
                    {
                        i.Item6.Invoke(nb, i.Item1);
                    }

                    Connection_CreateConnection(nb.fields[i.Item3].inPoint, cp_out, page);
                    Connection_CreateConnection(cp_in, nb.fields[i.Item4].outPoint, page);
                }
            }
        }

        public string Connection_CreateConnection(ConnectionPoint cp_in, ConnectionPoint cp_out, int page)
        {
            Vector2Int _in = Connection_GetConnectionInfo(cp_in);
            Vector2Int _out = Connection_GetConnectionInfo(cp_out);

            if (_in.x == _out.x) return "Cannot connect to itself";

            /* Field type check */
            if(Connection_IsFieldTypeIncludingCastingFeature(nodes[_in.x].fields[_in.y].fieldType, nodes[_out.x].fields[_out.y].fieldType))
            {
                Connection_FieldCastingFeature(cp_in, cp_out, page);
#if UNITY_EDITOR
                AssetDatabase.SaveAssets();
#endif
                return null;
            }
            else
            {
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

#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
#endif
            return null;
        }

        public bool Connection_IsFieldTypeIncludingCastingFeature(FieldType t1, FieldType t2)
        {
            foreach(var i in castingFeature)
            {
                if (i.Item1 == t1 && i.Item2 == t2) return true;
            }
            return false;
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
#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
#endif
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

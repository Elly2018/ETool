using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using ETool.ANode;

namespace ETool
{
    [CreateAssetMenu(menuName = "ETool/Blueprint")]
    public class EBlueprint : ScriptableObject
    {
        public const int DefaultPageCount = 3;

        /* Define how many sisters we have =///= */
        public List<NodeBase> nodes = new List<NodeBase>();
        public List<Connection> connections = new List<Connection>();
        public BlueprintEventStruct blueprintEvent;
        public List<BlueprintVariable> blueprintVariables = new List<BlueprintVariable>();

        private List<NodeBase> nodes_instance = new List<NodeBase>();
        private BlueprintInput pi;

        public void EOnValidate(GameObject g)
        {
            nodes = InitializeBlueprint(nodes, blueprintVariables, blueprintEvent.customEvent);
        }

        public void EStart(GameObject g)
        {
            nodes_instance = InitializeBlueprint(nodes, blueprintVariables, blueprintEvent.customEvent);
            pi = new BlueprintInput(nodes_instance.ToArray(), connections.ToArray(), g, VCopyTo(blueprintVariables), blueprintEvent.customEvent);

            Node c = GetNode(typeof(AConstructor), nodes_instance);
            Node a = GetNode(typeof(AStart), nodes_instance);

            if (c != null)
                c.ProcessCalling(pi);
            if (a != null)
                a.ProcessCalling(pi);
        }

        public void EStop(GameObject g)
        {
        }

        public void EUpdate(GameObject g)
        {
            Node a = GetNode(typeof(AUpdate), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(pi);
            }
        }

        public void EFixedUpdate(GameObject g)
        {
            Node a = GetNode(typeof(AFixedUpdate), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(pi);
            }
        }

        public void EOnDestroy(GameObject g)
        {
            Node a = GetNode(typeof(AOnDestory), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(pi);
            }
        }

        public void EOnCollisionEnter(Collision collision)
        {
            pi.m_Collision = collision;
            Node a = GetNode(typeof(AOnCollisionEnter), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(pi);
            }
        }

        public void EOnCollisionExit(Collision collision)
        {
            pi.m_Collision = collision;
            Node a = GetNode(typeof(AOnCollisionExit), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(pi);
            }
        }

        public void EOnCollisionStay(Collision collision)
        {
            pi.m_Collision = collision;
            Node a = GetNode(typeof(AOnCollisionStay), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(pi);
            }
        }

        public void EOnCollisionEnter2D(Collision2D collision)
        {
            pi.m_Collision2D = collision;
        }

        public void EOnCollisionExit2D(Collision2D collision)
        {
            pi.m_Collision2D = collision;
        }

        public void EOnCollisionStay2D(Collision2D collision)
        {
            pi.m_Collision2D = collision;
        }

        public void EOnTriggerEnter(Collider other)
        {
            pi.m_Collider = other;
        }

        public void EOnTriggerExit(Collider other)
        {
            pi.m_Collider = other;
        }

        public void EOnTriggerStay(Collider other)
        {
            pi.m_Collider = other;
        }

        public void EOnTriggerEnter2D(Collider2D collision)
        {
            pi.m_Collider2D = collision;
        }

        public void EOnTriggerExit2D(Collider2D collision)
        {
            pi.m_Collider2D = collision;
        }

        public void EOnTriggerStay2D(Collider2D collision)
        {
            pi.m_Collider2D = collision;
        }

        #region Function
        private Node GetNode(Type t, List<NodeBase> ns)
        {
            for (int i = 0; i < ns.Count; i++)
            {
                if (ns[i].NodeType == t.FullName) return ns[i];
            }
            return null;
        }

        public void DeleteCustomEvent(int index)
        {
            try
            {
                foreach (var i in nodes)
                {
                    if (i.page == index + 2)
                    {
                        foreach (var j in connections)
                        {
                            if (j.outPointMark.x == nodes.IndexOf(i) || j.inPointMark.x == nodes.IndexOf(i))
                            {
                                connections.Remove(j);
                            }
                        }
                        nodes.Remove(i);
                    }
                }
            }
            catch
            {

            }
            
        }

        public void DeleteCustomEventArugment(int index, int aindex)
        {
            try
            {
                foreach (var j in connections)
                {
                    if (j.outPointMark.x == index || j.inPointMark.x == index)
                    {
                        if (j.outPointMark.y == aindex || j.inPointMark.y == aindex)
                            connections.Remove(j);
                    }
                }
            }
            catch { }
        }

        public void ChangeCustomEventArugment(int index, int aindex)
        {
            try
            {
                foreach (var j in connections)
                {
                    if (j.outPointMark.x == index || j.inPointMark.x == index)
                    {
                        if (j.outPointMark.y == aindex || j.inPointMark.y == aindex)
                            connections.Remove(j);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Initialize node list
        /// </summary>
        /// <param name="useNode">Source Nodes</param>
        /// <param name="variable">Blueprint variables</param>
        /// <returns></returns>
        public static List<NodeBase> InitializeBlueprint(List<NodeBase> useNode, List<BlueprintVariable> variable, List<BlueprintCustomEvent> blueprintCustomEvents)
        {
            /* Empty result instance */
            List<NodeBase> result = new List<NodeBase>();

            /* Loop all nodes */
            for (int i = 0; i < useNode.Count; i++)
            {
                NodeBase nb = MakeInstanceNode(useNode[i], variable, blueprintCustomEvents);
                result.Add(nb);
            }

            return result;
        }

        public static NodeBase MakeInstanceNode(NodeBase soruce, List<BlueprintVariable> variable, List<BlueprintCustomEvent> blueprintCustomEvents)
        {
            Assembly ass = Assembly.GetExecutingAssembly();

            /* Node recreate apply the type */
            List<object> _args = new List<object>();
            _args.Add(soruce.rect.position);
            _args.Add(soruce.rect.width);
            _args.Add(soruce.rect.height);

            NodeBase nb = (NodeBase)ass.CreateInstance(
                    soruce.NodeType,
                    false,
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    _args.ToArray(),
                    null,
                    null);

            nb.page = soruce.page;
            nb.targetPage = soruce.targetPage;
            /* Node first time initialize */
            nb.Initialize();

            /* Post initialize usually handle blueprint data */
            nb.PostFieldInitialize(new BlueprintInput(null, null, null, variable, blueprintCustomEvents));
            if (nb != null)
            {

                /* Apply variable into field */
                for (int j = 0; j < nb.fields.Count; j++)
                {
                    nb.fields[j] = new Field(soruce.fields[j]);
                }

                /* Dynamic field initialize */
                nb.DynamicFieldInitialize(new BlueprintInput(null, null, null, variable, blueprintCustomEvents));

                /* Apply the dynamic field variable */
                for (int j = 0; j < nb.fields.Count; j++)
                {
                    nb.fields[j] = new Field(soruce.fields[j]);
                }

                /* We apply twice variable is because field is dynamic, we don't know the length of the fields */
                /* So after initialize dynamic field we apply the variable again */
                /* In case it miss value */
            }

            return nb;
        }

        private NodeBase SearchNodeByNode(List<NodeBase> reference, List<NodeBase> instance, NodeBase referenceNode)
        {
            if (reference.Contains(referenceNode))
            {
                return instance[reference.IndexOf(referenceNode)];
            }
            return null;
        }

        private Field SearchFieldByField(List<Field> reference, List<Field> instance, Field referenceField)
        {
            if (reference.Contains(referenceField))
            {
                return instance[reference.IndexOf(referenceField)];
            }
            return null;
        }

        /// <summary>
        /// Clone a variable list
        /// </summary>
        /// <param name="source">Source Variable List</param>
        /// <returns></returns>
        private static List<BlueprintVariable> VCopyTo(List<BlueprintVariable> source)
        {
            List<BlueprintVariable> result = new List<BlueprintVariable>();
            for(int i = 0; i < source.Count; i++)
            {
                BlueprintVariable bv = new BlueprintVariable();
                bv.label = source[i].label;
                bv.type = source[i].type;
                bv.variable = new GenericObject(source[i].variable);
                result.Add(bv);
            }
            return result;
        }

        #endregion
    }

    #region Struct

    [System.Serializable]
    public class BlueprintEventStruct
    {
        public bool startEvent;
        public bool updateEvent;
        public bool fixedUpdateEvent;
        public bool onDestroyEvent;
        public BlueprintPhysicsEvent physicsEvent; 
        public List<BlueprintCustomEvent> customEvent;
    }

    [System.Serializable]
    public class BlueprintPhysicsEvent
    {
        public bool onCollisionEnter = false;
        public bool onCollisionExit = false;
        public bool onCollisionStay = false;

        public bool onCollisionEnter2D = false;
        public bool onCollisionExit2D = false;
        public bool onCollisionStay2D = false;

        public bool onTriggerEnter = false;
        public bool onTriggerExit = false;
        public bool onTriggerStay = false;

        public bool onTriggerEnter2D = false;
        public bool onTriggerExit2D = false;
        public bool onTriggerStay2D = false;
    }

    [System.Serializable]
    public class BlueprintCustomEvent
    {
        public bool fold = false;
        public string eventName = "New Event";
        public List<BlueprintVariable> arugments = new List<BlueprintVariable>();
        public FieldType returnType;
    }

    [System.Serializable]
    public class BlueprintVariable
    {
        public FieldType type;
        public string label;
        public GenericObject variable;
    }

    public class BlueprintInput
    {
        public NodeBase[] allNode;
        public Connection[] allConnection;
        public GameObject thisGameobject;

        public List<BlueprintVariable> blueprintVariables;
        public List<BlueprintCustomEvent> blueprintCustomEvents;

        public Collision m_Collision;
        public Collision2D m_Collision2D;
        public Collider m_Collider;
        public Collider2D m_Collider2D;

        public BlueprintInput(NodeBase[] allnode, Connection[] allconnection, GameObject gameobj, List<BlueprintVariable> blueprintVariables, List<BlueprintCustomEvent> blueprintCustomEvents)
        {
            allNode = allnode;
            allConnection = allconnection;
            thisGameobject = gameobj;
            this.blueprintVariables = blueprintVariables;
            this.blueprintCustomEvents = blueprintCustomEvents;
        }
    }

    #endregion
}
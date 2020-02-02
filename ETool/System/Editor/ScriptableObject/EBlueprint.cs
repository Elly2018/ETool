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
        public BlueprintEventStruct blueprintEvent = new BlueprintEventStruct();
        public List<BlueprintVariable> blueprintVariables = new List<BlueprintVariable>();
        public EBlueprint Inherit = null;
        public BlueprintInput pi = null;

        private List<NodeBase> nodes_instance = new List<NodeBase>();

        #region Initialize
        /// <summary>
        /// Make a new instance of blueprint <br />
        /// This will called when every time object use blueprint <br />
        /// We do not want any object apply blueprint sample at runtime
        /// </summary>
        /// <param name="reference">Reference Blueprint</param>
        /// <param name="g">Self GameObject</param>
        /// <param name="b">Register GameObject</param>
        public void EBlueprintClone(EBlueprint reference, GameObject g, BlueprintGameobjectRegister[] b)
        {
            nodes = reference.nodes;
            connections = reference.connections;
            blueprintEvent = reference.blueprintEvent;
            blueprintVariables = reference.blueprintVariables;
            Inherit = reference.Inherit;
            InitializeProperties(g, b);
        }

        /// <summary>
        /// Initialize the blueprint properties <br />
        /// Such as make new instance of node, and connection etc.. <br />
        /// This called before blueprint execution
        /// </summary>
        /// <param name="g">Self GameObject</param>
        /// <param name="b">Register GameObject</param>
        public void InitializeProperties(GameObject g, BlueprintGameobjectRegister[] b)
        {
            nodes_instance = InitializeBlueprint(nodes, blueprintVariables, blueprintEvent.customEvent);
            pi = new BlueprintInput(nodes_instance.ToArray(), connections.ToArray(), g, VCopyTo(blueprintVariables), blueprintEvent.customEvent);
            pi.gameobjectRegister = b;

            if (Inherit != null)
            {
                EBlueprint buffer = EBlueprint.CreateInstance<EBlueprint>();
                buffer.EBlueprintClone(Inherit, g, b);
                pi.inherit = buffer;
            }
        }
        #endregion

        #region Call
        public void EOnValidate(GameObject g, BlueprintGameobjectRegister[] b)
        {
            nodes = InitializeBlueprint(nodes, blueprintVariables, blueprintEvent.customEvent);
        }

        public void EStart(GameObject g, BlueprintGameobjectRegister[] b)
        {
            InitializeProperties(g, b);

            Node c = GetNode(typeof(AConstructor), nodes_instance);
            Node a = GetNode(typeof(AStart), nodes_instance);

            if (c != null)
                c.ProcessCalling(pi);
            if (a != null)
                a.ProcessCalling(pi);
        }

        

        public void EStop(GameObject g, BlueprintGameobjectRegister[] b)
        {
        }

        public void EUpdate(GameObject g, BlueprintGameobjectRegister[] b)
        {
            Node a = GetNode(typeof(AUpdate), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(pi);
            }
        }

        public void EFixedUpdate(GameObject g, BlueprintGameobjectRegister[] b)
        {
            Node a = GetNode(typeof(AFixedUpdate), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(pi);
            }
        }

        public void EOnDestroy(GameObject g, BlueprintGameobjectRegister[] b)
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
        #endregion

        public bool CallCustomEvent(EBlueprint data, string EventName, object[] _arg)
        {
            foreach (var i in data.blueprintEvent.customEvent)
            {
                if(i.eventName == EventName)
                {
                    foreach(var j in data.pi.allNode)
                    {
                        if (j.GetType() == typeof(ACustomEvent) && j.page == data.blueprintEvent.customEvent.IndexOf(i) + EBlueprint.DefaultPageCount)
                        {
                            ACustomEvent buffer = (ACustomEvent)j;
                            buffer.ReceivedObject(_arg);
                            buffer.ProcessCalling(data.pi);
                            return true;
                        }
                    }
                }
            }
            if(data.Inherit != null)
            {
                return data.Inherit.CallCustomEvent(data, EventName, _arg);
            }
            else
            {
                return false;
            }
        }

        public bool CallSetVariable(EBlueprint data, int index, FieldType type, object o)
        {
            Field.SetObjectByFieldType(type, data.pi.blueprintVariables[index].variable, o);
            return true;
        }

        public object CallGetVariable(EBlueprint data, int index, FieldType type)
        {
            return Field.GetObjectByFieldType(type, data.pi.blueprintVariables[index].variable);
        }

        private Node GetNode(Type t, List<NodeBase> ns)
        {
            for (int i = 0; i < ns.Count; i++)
            {
                if (ns[i].NodeType == t.FullName) return ns[i];
            }
            return null;
        }

        #region GUI Function
        /// <summary>
        /// When user delete custom event <br />
        /// It remove any relate event node and relate connection
        /// </summary>
        /// <param name="index">Index Of Custom Event</param>
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

        public void DeleteCustomVariable(int index)
        {
            try
            {
                foreach (var i in nodes)
                {
                    if (i.GetType() == typeof(SetVariable))
                    {
                        if ((int)i.fields[0].GetValue(FieldType.Variable) == index)
                        {
                            foreach (var j in connections)
                            {
                                if (j.inPointMark.x == nodes.IndexOf(i) || j.outPointMark.x == nodes.IndexOf(i))
                                {
                                    connections.Remove(j);
                                }
                            }
                        }
                    }

                    if (i.GetType() == typeof(GetVariable))
                    {
                        if ((int)i.fields[0].GetValue(FieldType.Variable) == index)
                        {
                            foreach (var j in connections)
                            {
                                if (j.inPointMark.x == nodes.IndexOf(i) || j.outPointMark.x == nodes.IndexOf(i))
                                {
                                    connections.Remove(j);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public void ChangeCustomVariableType(int index)
        {
            try
            {
                foreach (var i in nodes)
                {
                    if (i.GetType() == typeof(SetVariable))
                    {
                        if ((int)i.fields[0].GetValue(FieldType.Variable) == index)
                        {
                            foreach (var j in connections)
                            {
                                if (j.inPointMark.x == nodes.IndexOf(i) || j.outPointMark.x == nodes.IndexOf(i))
                                {
                                    connections.Remove(j);
                                }
                            }
                            i.PostFieldInitialize();
                        }
                    }

                    if (i.GetType() == typeof(GetVariable))
                    {
                        if ((int)i.fields[0].GetValue(FieldType.Variable) == index)
                        {
                            foreach (var j in connections)
                            {
                                if (j.inPointMark.x == nodes.IndexOf(i) || j.outPointMark.x == nodes.IndexOf(i))
                                {
                                    connections.Remove(j);
                                }
                            }
                            i.PostFieldInitialize();
                        }
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
        #endregion

        #region Getter
        public List<BlueprintCustomEvent> GetAllPublicEvent()
        {
            if (Inherit)
            {
                List<BlueprintCustomEvent> buffer = new List<BlueprintCustomEvent>();
                buffer.AddRange(GetPublicEvent());
                buffer.AddRange(Inherit.GetPublicEvent());
                return buffer;
            }
            else
            {
                return GetPublicEvent();
            }
        }

        public List<BlueprintCustomEvent> GetInheritEvent()
        {
            if (Inherit)
            {
                List<BlueprintCustomEvent> buffer = new List<BlueprintCustomEvent>();
                buffer.AddRange(GetPublic_Protect_Event());
                buffer.AddRange(Inherit.GetInheritEvent());
                return buffer;
            }
            else
            {
                return GetPublic_Protect_Event();
            }
        }


        public List<BlueprintCustomEvent> GetPublicEvent()
        {
            List<BlueprintCustomEvent> buffer = new List<BlueprintCustomEvent>();
            foreach (var i in blueprintEvent.customEvent)
            {
                if (i.accessAbility == AccessAbility.Public)
                {
                    buffer.Add(i);
                }
            }
            return buffer;
        }

        public List<BlueprintCustomEvent> GetPublic_Protect_Event()
        {
            List<BlueprintCustomEvent> buffer = new List<BlueprintCustomEvent>();
            foreach(var i in blueprintEvent.customEvent)
            {
                if(i.accessAbility == AccessAbility.Public || i.accessAbility == AccessAbility.Protected)
                {
                    buffer.Add(i);
                }
            }
            return buffer;
        }

        public List<BlueprintVariable> GetInheritVariable()
        {
            if (Inherit)
            {
                List<BlueprintVariable> buffer = new List<BlueprintVariable>();
                buffer.AddRange(GetPublic_Protect_Variable());
                buffer.AddRange(Inherit.GetInheritVariable());
                return buffer;
            }
            else
            {
                return GetPublic_Protect_Variable();
            }
        }

        public List<BlueprintVariable> GetPublic_Protect_Variable()
        {
            List<BlueprintVariable> buffer = new List<BlueprintVariable>();
            foreach(var i in blueprintVariables)
            {
                if (i.accessAbility == AccessAbility.Public || i.accessAbility == AccessAbility.Protected)
                {
                    buffer.Add(i);
                }
            }
            return buffer;
        }

        public List<BlueprintVariable> GetAllPublicVariable()
        {
            if (Inherit)
            {
                List<BlueprintVariable> buffer = new List<BlueprintVariable>();
                buffer.AddRange(GetPublicVariable());
                buffer.AddRange(Inherit.GetPublicVariable());
                return buffer;
            }
            else
            {
                return GetPublicVariable();
            }
        }

        public List<BlueprintVariable> GetPublicVariable()
        {
            List<BlueprintVariable> buffer = new List<BlueprintVariable>();
            foreach (var i in blueprintVariables)
            {
                if (i.accessAbility == AccessAbility.Public)
                {
                    buffer.Add(i);
                }
            }
            return buffer;
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
            nb.title = soruce.title;
            nb.unlocalTitle = soruce.unlocalTitle;
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
                    try
                    {
                        nb.fields[j] = new Field(soruce.fields[j]);
                    }catch(Exception e) { 
                        Debug.LogWarning(e.Message);
                        Debug.LogWarning(nb.title + " Node ID: " + j);
                    }
                }

                nb.FinalFieldInitialize(new BlueprintInput(null, null, null, variable, blueprintCustomEvents));

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
                bv.fieldContainer = source[i].fieldContainer;
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
        public BlueprintPhysicsEvent physicsEvent = new BlueprintPhysicsEvent(); 
        public List<BlueprintCustomEvent> customEvent = new List<BlueprintCustomEvent>();
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
        public AccessAbility accessAbility;
        public bool fold = false;
        public string eventName = "New Event";
        public List<BlueprintVariable> arugments = new List<BlueprintVariable>();
        public FieldType returnType;
    }

    [System.Serializable]
    public class BlueprintVariable
    {
        public AccessAbility accessAbility;
        public FieldType type;
        public FieldContainer fieldContainer;
        public string label;
        public bool fold;
        public GenericObject variable = new GenericObject();
        public GenericObject[] variable_Array = new GenericObject[0];
    }

    public class BlueprintInput
    {
        public NodeBase[] allNode;
        public Connection[] allConnection;
        public GameObject thisGameobject;
        public BlueprintGameobjectRegister[] gameobjectRegister;
        public EBlueprint inherit;

        public List<BlueprintVariable> blueprintVariables = new List<BlueprintVariable>();
        public List<BlueprintCustomEvent> blueprintCustomEvents = new List<BlueprintCustomEvent>();
        public List<FieldType> returnType = new List<FieldType>();

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
            InitArray();
        }

        private void InitArray()
        {
            foreach(var i in blueprintVariables)
            {
                if(i.fieldContainer == FieldContainer.Array)
                {
                    i.variable_Array = new GenericObject[i.variable.genericBasicType.target_Int];

                    for(int j = 0;  j < i.variable_Array.Length; j++)
                    {
                        i.variable_Array[j] = new GenericObject();
                    }
                }
            }
        }
    }

    #endregion
}
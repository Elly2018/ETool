using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using ETool.ANode;

namespace ETool
{
    /// <summary>
    /// 
    /// Blueprint <br />
    /// 
    /// The major object contain all the nodes and connections and event information <br />
    /// There are some partial class that handle different part of mechanism <br />
    /// <list type="bullet">
    /// <item> Node : handle nodes create, modify, delete </item>
    /// <item> Connection : handle relationship between nodes </item>
    /// <item> Call : handle unity pipeline call event </item>
    /// <item> GUI : handle node editor event </item>
    /// <item> Editor : handle inspetor editor event </item>
    /// <item> Check : handle state check, most of time use for update error fix </item>
    /// <item> Custom : handle custom object create, modify, delete, getter </item>
    /// <item> Struct : store the struct that will use in the working pipeline </item>
    /// </list>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "ETool/Blueprint")]
    public partial class EBlueprint : ScriptableObject
    {
        /// <summary>
        /// Normally node editor have 3 page at the top <br />
        /// <list type="bullet">
        /// <item> Main Editor </item>
        /// <item> Constructor </item>
        /// <item> Physics </item>
        /// </list>
        /// </summary>
        public const int DefaultPageCount = 3;


        /// <summary>
        /// 
        /// Store nodes <br />
        /// 
        /// Warning : unity doesn't support c# generic class serialize <br />
        /// This will cause some bug occur <br />
        /// All node type it will read as <see cref="NodeBase"/> <br />
        /// So when GUI update we will call initialize node <br />
        /// Keep node as instance
        /// 
        /// </summary>
        public List<NodeBase> nodes = new List<NodeBase>();


        /// <summary>
        /// Store connections
        /// </summary>
        public List<Connection> connections = new List<Connection>();


        /// <summary>
        /// This struct contain all the events we need <br />
        /// <list type="bullet">
        /// <item> Unity pipeline event </item>
        /// <item> Custom event </item>
        /// </list>
        /// </summary>
        public BlueprintEventStruct blueprintEvent = new BlueprintEventStruct();


        /// <summary>
        /// 
        /// This struct contain all the custom variables we need <br />
        /// 
        /// In blueprint editor you can use <see cref="GetVariable"/> and <see cref="SetVariable"/> get the reference of this variable <br />
        /// If you trying to get the reference of other blueprint <br />
        /// Please check <see cref="SetOutterVariable"/> and <see cref="GetOutterVariable"/>
        /// 
        /// </summary>
        public List<BlueprintVariable> blueprintVariables = new List<BlueprintVariable>();


        /// <summary>
        /// 
        /// Inherit Blueprint <br />
        /// 
        /// Blueprint will inherit the custom variable and method <br />
        /// 
        /// </summary>
        public EBlueprint Inherit = null;


        /// <summary>
        /// The instance of blueprint input information <br />
        /// It contain what it need in blueprint execute at running time <br />
        /// </summary>
        public BlueprintInput _InputInstance = null;


        /// <summary>
        /// Nodes instance buffer
        /// </summary>
        private List<NodeBase> nodes_instance = new List<NodeBase>();


        /// <summary>
        /// Simply get the assembly object
        /// </summary>
        public Assembly assembly
        {
            get
            {
                return Assembly.GetExecutingAssembly();
            }
        }


        /// <summary>
        /// It will return all the blueprint scriptable object in the assets
        /// </summary>
        public static EBlueprint[] GetAllBlueprint
        {
            get
            {
                return Resources.FindObjectsOfTypeAll<EBlueprint>();
            }
        }


        public static List<EBlueprint> AllInstance = new List<EBlueprint>();

        ///
        ///  Initialize Stage
        ///
        #region Initialize
        /// <summary>
        /// Make a new instance of blueprint <br />
        /// In Running time, object only get the reference of instance reference <br />
        /// not blueprint class reference
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
            nodes_instance = InitializeBlueprint(nodes, blueprintVariables, blueprintEvent.customEvent, Inherit);
            _InputInstance = new BlueprintInput(nodes_instance.ToArray(), connections.ToArray(), g, VCopyTo(blueprintVariables), blueprintEvent.customEvent);
            _InputInstance.self = this;
            _InputInstance.gameobjectRegister = b;

            if (Inherit != null)
            {
                EBlueprint buffer = EBlueprint.CreateInstance<EBlueprint>();
                buffer.EBlueprintClone(Inherit, g, b);
                buffer.name = Inherit.name;
                _InputInstance.inherit = buffer;
            }
        }
        #endregion


        private Node GetNode(Type t, List<NodeBase> ns)
        {
            for (int i = 0; i < ns.Count; i++)
            {
                if (ns[i].NodeType == t.FullName) return ns[i];
            }
            return null;
        }

        public int GetSelectionNodeCount()
        {
            int result = 0;
            foreach (var i in nodes)
            {
                if (i.isSelected) result++;
            }
            return result;
        }

        #region Getter
        public List<Tuple<BlueprintCustomEvent, EBlueprint>> GetAllPublicEvent()
        {
            if (Inherit)
            {
                List<Tuple<BlueprintCustomEvent, EBlueprint>> buffer = new List<Tuple<BlueprintCustomEvent, EBlueprint>>();
                buffer.AddRange(GetPublicEvent());
                buffer.AddRange(Inherit.GetPublicEvent());
                return buffer;
            }
            else
            {
                return GetPublicEvent();
            }
        }

        public List<Tuple<BlueprintCustomEvent, EBlueprint>> GetInheritEvent()
        {
            if (Inherit)
            {
                List<Tuple<BlueprintCustomEvent, EBlueprint>> buffer = new List<Tuple<BlueprintCustomEvent, EBlueprint>>();
                buffer.AddRange(GetPublic_Protect_Event());
                buffer.AddRange(Inherit.GetInheritEvent());
                return buffer;
            }
            else
            {
                return GetPublic_Protect_Event();
            }
        }


        public List<Tuple<BlueprintCustomEvent, EBlueprint>> GetOnlyInheritEvent()
        {
            if (Inherit)
            {
                List<Tuple<BlueprintCustomEvent, EBlueprint>> buffer = new List<Tuple<BlueprintCustomEvent, EBlueprint>>();
                buffer.AddRange(Inherit.GetInheritEvent());
                return buffer;
            }
            else
            {
                return null;
            }
        }

        public List<Tuple<BlueprintCustomEvent, EBlueprint>> GetPublicEvent()
        {
            List<Tuple<BlueprintCustomEvent, EBlueprint>> buffer = new List<Tuple<BlueprintCustomEvent, EBlueprint>>();
            foreach (var i in blueprintEvent.customEvent)
            {
                if (i.accessAbility == AccessAbility.Public)
                {
                    buffer.Add(new Tuple<BlueprintCustomEvent, EBlueprint>(i, this));
                }
            }
            return buffer;
        }

        public List<Tuple<BlueprintCustomEvent, EBlueprint>> GetPublic_Protect_Event()
        {
            List<Tuple<BlueprintCustomEvent, EBlueprint>> buffer = new List<Tuple<BlueprintCustomEvent, EBlueprint>>();
            foreach(var i in blueprintEvent.customEvent)
            {
                if(i.accessAbility == AccessAbility.Public || i.accessAbility == AccessAbility.Protected)
                {
                    buffer.Add(new Tuple<BlueprintCustomEvent, EBlueprint>(i, this));
                }
            }
            return buffer;
        }

        public List<Tuple<BlueprintVariable, EBlueprint>> GetInheritVariable()
        {
            if (Inherit)
            {
                List<Tuple<BlueprintVariable, EBlueprint>> buffer = new List<Tuple<BlueprintVariable, EBlueprint>>();
                buffer.AddRange(GetPublic_Protect_Variable());
                buffer.AddRange(Inherit.GetInheritVariable());
                return buffer;
            }
            else
            {
                return GetPublic_Protect_Variable();
            }
        }

        public List<Tuple<BlueprintVariable, EBlueprint>> GetPublic_Protect_Variable()
        {
            List<Tuple<BlueprintVariable, EBlueprint>> buffer = new List<Tuple<BlueprintVariable, EBlueprint>>();
            foreach(var i in blueprintVariables)
            {
                if (i.accessAbility == AccessAbility.Public || i.accessAbility == AccessAbility.Protected)
                {
                    buffer.Add(new Tuple<BlueprintVariable, EBlueprint>(i, this));
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

        public static EBlueprint GetBlueprintInstanceByName(string name)
        {
            foreach(var i in GetAllBlueprint)
            {
                if (i.name == name) return i;
            }
            return null;
        }

        /// <summary>
        /// Initialize node list
        /// </summary>
        /// <param name="useNode">Source Nodes</param>
        /// <param name="variable">Blueprint variables</param>
        /// <returns></returns>
        public static List<NodeBase> InitializeBlueprint(List<NodeBase> useNode, List<BlueprintVariable> variable, List<BlueprintCustomEvent> blueprintCustomEvents, EBlueprint inherit)
        {
            /* Empty result instance */
            List<NodeBase> result = new List<NodeBase>();

            /* Loop all nodes */
            for (int i = 0; i < useNode.Count; i++)
            {
                NodeBase nb = MakeInstanceNode(useNode[i], variable, blueprintCustomEvents, inherit);
                result.Add(nb);
            }

            return result;
        }

        public static NodeBase MakeInstanceNode(NodeBase soruce, List<BlueprintVariable> variable, List<BlueprintCustomEvent> blueprintCustomEvents, EBlueprint inherit)
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

            nb.GivenValue(soruce);

            /* Node first time initialize */
            nb.Initialize();

            /* Post initialize usually handle blueprint data */
            nb.PostFieldInitialize(new BlueprintInput(null, null, null, variable, blueprintCustomEvents) { inherit = inherit });
            if (nb != null)
            {

                /* Apply variable into field */
                for (int j = 0; j < nb.fields.Count; j++)
                {
                    nb.fields[j] = new Field(soruce.fields[j]);
                }

                /* Dynamic field initialize */
                nb.DynamicFieldInitialize(new BlueprintInput(null, null, null, variable, blueprintCustomEvents) { inherit = inherit });

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
        public bool lateUpdateEvent;
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
        public FieldContainer returnContainer;
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
        public EBlueprint self;
        public EBlueprint inherit;
        public BlueprintEventManager eventManager = new BlueprintEventManager();

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
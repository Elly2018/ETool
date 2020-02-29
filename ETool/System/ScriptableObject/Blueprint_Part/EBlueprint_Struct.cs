using System;
using System.Collections.Generic;
using UnityEngine;
using ETool.ANode;

/// <summary>
/// 
/// Blueprint Struct
/// 
/// This file contain all the enum and struct that will use in node editor GUI
/// Most of the structs are use for sending message package
/// 
/// </summary>
namespace ETool
{
    /// <summary>
    /// 
    /// Add Click Event <br />
    /// 
    /// Tell the Blueprint object <br />
    /// You want to add a node
    /// 
    /// </summary>
    public struct AddClickEvent
    {
        /// <summary>
        /// Adding position
        /// </summary>
        public Vector2 mousePosition;

        /// <summary>
        /// Adding node type
        /// </summary>
        public Type add;

        /// <summary>
        /// Adding page
        /// </summary>
        public int page;

        /// <summary>
        /// Struct constructor
        /// </summary>
        /// <param name="mousePosition">Adding Position</param>
        /// <param name="add">Adding Node Type</param>
        /// <param name="page">Adding Page</param>
        public AddClickEvent(Vector2 mousePosition, Type add, int page)
        {
            this.mousePosition = mousePosition;
            this.add = add;
            this.page = page;
        }
    }


    /// <summary>
    /// 
    /// Add Custom Event <br />
    /// 
    /// Custom Event is a special object <br />
    /// It require <see cref="BlueprintCustomEvent"/> object as argument <br />
    /// Blueprint will according to <see cref="BlueprintCustomEvent"/> object to setup <br />
    /// 
    /// User also need to given a target string <br />
    /// Which is a string that represent your blueprint method <br />
    /// Format : [Blueprint Name].[Blueprint Custom Event Name] <br />
    /// 
    /// </summary>
    public struct AddCustomEvent
    {
        /// <summary>
        /// Adding position
        /// </summary>
        public Vector2 mousePosition;

        /// <summary>
        /// Target blueprint
        /// </summary>
        public EBlueprint tbp;

        /// <summary>
        /// Target event
        /// </summary>
        public BlueprintCustomEvent bce;

        /// <summary>
        /// Adding page
        /// </summary>
        public int page;

        /// <summary>
        /// Define the event node is inherit
        /// </summary>
        public bool isInherit;

        /// <summary>
        /// Struct constructor
        /// </summary>
        /// <param name="mousePosition">Adding Position</param>
        /// <param name="tbp">Target Blueprint</param>
        /// <param name="bce">Target Event</param>
        /// <param name="page">Adding Page</param>
        public AddCustomEvent(Vector2 mousePosition, EBlueprint tbp, BlueprintCustomEvent bce, int page, bool isinherit)
        {
            this.mousePosition = mousePosition;
            this.bce = bce;
            this.tbp = tbp;
            this.page = page;
            this.isInherit = isinherit;
        }
    }


    /// <summary>
    /// 
    /// EventNodeType <br />
    /// 
    /// This enum only contain unity event enum <br />
    /// The purpose of this enum is use for check blueprint state <br />
    /// 
    /// Enum element follow the format : ETool.ANode.A + [Enum Element String] <br /> 
    /// For example : <see cref="AStart"/> This class full name is ETool.ANode.AStart <br />
    /// And in enum element list, it's pair name is "Start" <br />
    /// Each unity event have a prefix "A", so it's easier to search
    /// 
    /// </summary>
    public enum EventNodeType
    {
        Start,
        Update,
        FixedUpdate,
        LateUpdate,
        OnDestory,
        Constructor,

        OnCollisionEnter,
        OnCollisionExit,
        OnCollisionStay,

        OnCollisionEnter2D,
        OnCollisionExit2D,
        OnCollisionStay2D,

        OnTriggerEnter,
        OnTriggerExit,
        OnTriggerStay,

        OnTriggerEnter2D,
        OnTriggerExit2D,
        OnTriggerStay2D
    }


    /// <summary>
    /// 
    /// Node Editor Message Popup <br />
    /// 
    /// It's use for <see cref="NodeBasedEditor"/> message popup <br />
    /// Sometime is showing the description of a node
    /// 
    /// </summary>
    public class NodeEditorMessagePopup
    {
        /// <summary>
        /// Define the popup have "ok" button
        /// </summary>
        public bool Okbutton;

        /// <summary>
        /// The center text content
        /// </summary>
        public string Message;
    }


    /// <summary>
    /// 
    /// Search Struct <br />
    /// 
    /// The struct is use for <see cref="NodeBasedEditor"/> searching node feature <br />
    /// 
    /// </summary>
    public class SearchStruct
    {
        /// <summary>
        /// The node type we select
        /// </summary>
        public Type type;

        /// <summary>
        /// The search bar user has enter
        /// </summary>
        public string inputField;
    }


    /// <summary>
    /// 
    /// </summary>
    public class TypeListStruct
    {
        public int typeListTypeSelection = 0;
        public FieldType fieldSelection = FieldType.Event;
        public Field target;
    }

    /// <summary>
    /// 
    /// For Node Name Sort <br />
    /// 
    /// A buffer store the node type and path string
    /// 
    /// </summary>
    public class ForNodeNameSort
    {
        /// <summary>
        /// Node type
        /// </summary>
        public Type type;

        /// <summary>
        /// Node path string
        /// </summary>
        public string nodepath;
    }


    /// <summary>
    /// 
    /// Clip borad <br />
    /// 
    /// The struct is use for <see cref="NodeBasedEditor"/> copying and pasting feature <br />
    /// 
    /// </summary>
    public class Clipborad
    {
        /// <summary>
        /// Nodes copy <br />
        /// Notice the objects store in here are instance, not reference
        /// </summary>
        public List<NodeBase> nodeBases;

        /// <summary>
        /// Connections copy <br />
        /// Notice the objects store in here are instance, not reference
        /// </summary>
        public List<Connection> connections;
    }


    /// <summary>
    /// 
    /// Zoom Data <br />
    /// 
    /// The struct is use for <see cref="NodeBasedEditor"/> zooming feature <br />
    /// 
    /// </summary>
    public struct ZoomData
    {
        /// <summary>
        /// The max value of zoom level
        /// </summary>
        public float maximum;

        /// <summary>
        /// The min value of zoom level
        /// </summary>
        public float minimum;

        /// <summary>
        /// The node position and its size will multiply this number
        /// </summary>
        public float ratio;

        /// <summary>
        /// Should the title be rendering
        /// </summary>
        public float titleHiddenLimit;

        /// <summary>
        /// Should the field be rendering
        /// </summary>
        public float fieldHiddenLimit;
    }
}

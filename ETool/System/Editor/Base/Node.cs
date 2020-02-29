using System;
using System.Collections.Generic;
using UnityEngine;
using ETool.ANode;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ETool
{
    /// <summary>
    /// Base class of node <br />
    /// Contain basic functionality
    /// </summary>
    [System.Serializable]
    public class Node
    {
        /// <summary>
        /// Each properties rect height
        /// </summary>
        public const float PropertiesHeight = 40;

        /// <summary>
        /// Node render rect
        /// </summary>
        public Rect rect;

        /// <summary>
        /// Node title string
        /// </summary>
        public string title;

        /// <summary>
        /// Node title string
        /// </summary>
        public string unlocalTitle;

        /// <summary>
        /// Node description <br />
        /// After click right click menu will show on screen
        /// </summary>
        public string description;

        /// <summary>
        /// Define render page index
        /// </summary>
        public int page;

        /// <summary>
        /// Define target event <br />
        /// Format: [blueprint name].[event name]
        /// </summary>
        public string targetEventOrVar;

        /// <summary>
        /// Define if event is inherit or private
        /// </summary>
        public bool isInherit;


        /// <summary>
        /// Is node is in drag mode
        /// </summary>
        public bool isDragged;


        /// <summary>
        /// Is node is in select mode
        /// </summary>
        public bool isSelected;


        /// <summary>
        /// Is there error in the node
        /// </summary>
        public List<NodeError> nodeErrors = new List<NodeError>();


        /// <summary>
        /// Node type as string <br />
        /// This string will use for create instance when check stage is enable <br />
        /// Because unity does not support inherit type serialize <br />
        /// So we store in the string, and after check stage we make new instance to cover the old one
        /// </summary>
        public string NodeType;


        /// <summary>
        /// Define node fields <br />
        /// The most inportant variable in the node
        /// </summary>
        public List<Field> fields = new List<Field>();

        public FieldType returnType;
        public FieldContainer returnContainer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">Node spawn position</param>
        /// <param name="width">Node width</param>
        /// <param name="height">Node height</param>
        public Node(Vector2 position, float width, float height)
        {
            rect = new Rect(position.x, position.y, width, height);
            NodeType = this.GetType().FullName;
            if (fields == null) fields = new List<Field>();
        }
        
        public void GivenValue(Node soruce)
        {
            page = soruce.page;
            title = soruce.title;
            unlocalTitle = soruce.unlocalTitle;
            returnType = soruce.returnType;
            returnContainer = soruce.returnContainer;
            targetEventOrVar = soruce.targetEventOrVar;
            isInherit = soruce.isInherit;
        }

        /// <summary>
        /// Initialize stage
        /// </summary>
        public void Initialize()
        {
            FieldInitialize();
        }

        #region Virtual Method
        /// <summary>
        /// Field initialize <br />
        /// Usually field will create at this stage
        /// </summary>
        public virtual void FieldInitialize() { }

        /// <summary>
        /// Post field initialize <br />
        /// Usually after initialize, this method will called
        /// </summary>
        public virtual void PostFieldInitialize() { }

        /// <summary>
        /// Post field initialize at runtime <br />
        /// Usually after initialize, this method will called
        /// </summary>
        /// <param name="data"></param>
        public virtual void PostFieldInitialize(BlueprintInput data) { }

        /// <summary>
        /// When there are dynamic field <br />
        /// Usually after initialize and post initialize, this method will called
        /// </summary>
        /// <param name="data"></param>
        public virtual void DynamicFieldInitialize(BlueprintInput data) { }

        /// <summary>
        /// When there are all initialize field <br />
        /// Usually after initialize and post initialize, this method will called
        /// </summary>
        /// <param name="data"></param>
        public virtual void FinalFieldInitialize(BlueprintInput data) { }

        /// <summary>
        /// When event is trigger <br />
        /// The node main execute method
        /// </summary>
        /// <param name="data"></param>
        public virtual void ProcessCalling(BlueprintInput data) { }

        /// <summary>
        /// When one of field is changed value <br />
        /// This method will called
        /// </summary>
        public virtual void FieldUpdate() { }
        public virtual void ConnectionUpdate() { }
        public virtual void SelectionChanged(bool change) { }
        public virtual void DragChanged(bool change) { }
        #endregion

#if UNITY_EDITOR
        /// <summary>
        /// Drag node method
        /// </summary>
        /// <param name="delta">Moving delta</param>
        public virtual void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        /// <summary>
        /// Drawing fields method
        /// </summary>
        /// <param name="fs">Target field list</param>
        public void DrawField(List<Field> fs)
        {
            ZoomData zoomLevel = NodeBasedEditor.Editor_Instance.GetZoomLevel();
            GUIStyle ti = StyleUtility.GetStyle(StyleType.GUI_Title);
            ti.padding.left = 7;
            ti.padding.right = 7;

            GUI.Label(new Rect(
                rect.x * zoomLevel.ratio,
                rect.y * zoomLevel.ratio,
                rect.width * zoomLevel.ratio,
                rect.height * zoomLevel.ratio),
                zoomLevel.titleHiddenLimit < zoomLevel.ratio ? EToolString.GetString_Node(EToolString.GetNodeTitle(GetType()), unlocalTitle) : "...",
                ti);

            for (int i = 0; i < fs.Count; i++)
            {
                /* Update field */
                fs[i].rect = new Rect((rect.x + 15f) * zoomLevel.ratio, (rect.y + rect.height + (PropertiesHeight * i)) * zoomLevel.ratio, (rect.width - 30f) * zoomLevel.ratio, (PropertiesHeight) * zoomLevel.ratio);
                fs[i].inPoint.rect = fs[i].rect;
                fs[i].outPoint.rect = fs[i].rect;

                if (zoomLevel.fieldHiddenLimit < zoomLevel.ratio)
                    fs[i].Draw();
                else
                    fs[i].DrawBG();
            }
        }

        /// <summary>
        /// Receive the editor event <br />
        /// Usually contain basic functionally of the node <br />
        /// Moving, Drag, Click
        /// </summary>
        /// <param name="e">Editor event</param>
        /// <returns></returns>
        public bool ProcessEvents(Event e)
        {
            ZoomData zoomLevel = NodeBasedEditor.Editor_Instance.GetZoomLevel();

            foreach (var i in fields)
            {
                i.ProcessEvent(e);
            }

            switch (e.type)
            {
                case EventType.MouseDown:
                    {
                        /* Right mouse */
                        if (e.button == 1 && isSelected && MouseIn(e.mousePosition))
                        {
                            ProcessContextMenu();
                            e.Use();
                            //isSelected = false;
                        }   

                        if (!e.shift && !NodeBasedEditor.Instance.Check_AnyOtherNodeAreSelected(this) && !MouseIn(e.mousePosition) && !NodeBasedEditor.Editor_Instance.MouseInMenuBar(e.mousePosition) && e.button == 0)
                        {
                            isSelected = false;
                            SelectionChanged(isSelected);
                            GUI.changed = true;
                        }
                        break;
                    }

                case EventType.MouseUp:
                    {
                        /* Left mouse */
                        if (e.button == 0)
                        {
                            if (MouseIn(e.mousePosition) && !isSelected && !isDragged)
                            {
                                GUI.changed = true;
                                GUI.FocusControl(null);
                                isSelected = true;
                                SelectionChanged(isSelected);
                            }
                            else if (MouseIn(e.mousePosition) && isSelected && !isDragged)
                            {
                                GUI.changed = true;
                                GUI.FocusControl(null);
                                isSelected = false;
                                SelectionChanged(isSelected);
                            }
                        }

                        isDragged = false;
                        DragChanged(isDragged);
                        break;
                    }

                case EventType.MouseMove:
                    {
                        if (isSelected) isDragged = true;
                        break;
                    }

                case EventType.MouseDrag:
                    {
                        if (e.button == 0 && isSelected)
                        {
                            isDragged = true;
                            DragChanged(isDragged);
                            EditorUtility.SetDirty(NodeBasedEditor.Instance);
                            Drag(e.delta * (1 / zoomLevel.ratio));
                            //e.Use();
                            return true;
                        }
                        break;
                    }
                    
            }

            return false;
        }

        public bool MouseIn(Vector2 pos)
        {
            ZoomData zoomLevel = NodeBasedEditor.Editor_Instance.GetZoomLevel();
            Rect buffer = new Rect(
                rect.x * zoomLevel.ratio,
                rect.y * zoomLevel.ratio,
                rect.width * zoomLevel.ratio,
                rect.height * zoomLevel.ratio);

            return buffer.Contains(pos);
        }

        /// <summary>
        /// When cursor is on node, and receive right click event
        /// </summary>
        public virtual void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();

            /* Adding description */
            {
                genericMenu.AddItem(new GUIContent("Description"), false, OnClickDescription);
            }

            /* Adding copy */
            {
                if (NodeBasedEditor.Instance.Check_AnyNodeSelect())
                {
                    genericMenu.AddItem(new GUIContent("Copy selection"), false, NodeBasedEditor.Editor_Instance.OnClickCopy);
                }
            }

            /* Adding delete selection */
            {
                if (NodeBasedEditor.Instance.Check_AnyConnectionSelect() || NodeBasedEditor.Instance.Check_AnyNodeSelect())
                {
                    genericMenu.AddItem(new GUIContent("Delete Selected"), false, NodeBasedEditor.Editor_Instance.DeleteSelection);
                }
            }

            /* Adding error message */
            {
                if (nodeErrors.Count != 0)
                {
                    foreach (var i in nodeErrors)
                    {
                        genericMenu.AddItem(new GUIContent("ErrorMessage: " + i.errorType.ToString() + i.code.ToString()), false, OnClickErrorMessage, i.errorString);
                    }
                }
            }

            if(NodeType == typeof(ACustomEventCall).FullName)
            {
                foreach(var i in NodeBasedEditor.Instance.GetAllPublicEvent())
                {
                    genericMenu.AddItem(new GUIContent("Change Event =>/" + i.Item2.name + "." + i.Item1.eventName), false, ChangeCustomEvent, i);
                }
            }

            genericMenu.AddDisabledItem(new GUIContent("Node Index: " + EBlueprint.GetNodeInBlueprintIndex(this as NodeBase)));

            genericMenu.ShowAsContext();
        }

        protected void ChangeCustomEvent(object o)
        {
            Tuple<BlueprintCustomEvent, EBlueprint> target = (Tuple<BlueprintCustomEvent, EBlueprint>)o;
            NodeBase nb = (NodeBase)this;
            if (nb != null)
            {
                nb.unlocalTitle = target.Item1.eventName;
                nb.targetEventOrVar = target.Item2 + "." + target.Item1;
            }
        }

        protected void ChangeInheritCustomEvent(object o)
        {
            string target = (string)o;
            NodeBase nb = (NodeBase)this;
            if (nb != null)
            {
                nb.unlocalTitle = target;
                nb.targetEventOrVar = "";
            }   
        }

        /// <summary>
        /// User want to check error message
        /// </summary>
        /// <param name="messageString"></param>
        protected void OnClickErrorMessage(object messageString)
        {
            NodeBasedEditor.Editor_Instance.DrawGreyBackgroundOkButton((string)messageString);
        }

        /// <summary>
        /// User want to check node description
        /// </summary>
        protected void OnClickDescription()
        {
            NodeBasedEditor.Editor_Instance.DrawGreyBackgroundOkButton(EToolString.GetString_Node(EToolString.GetNodeDes(GetType()), ""));
        }
#endif

        public void AddNodeError(NodeError ne)
        {
            foreach (var i in nodeErrors)
            {
                if (i == ne)
                {
                    return;
                }
            }
            nodeErrors.Add(ne);
            GUI.changed = true;
        }

        public void DeleteNodeError(NodeError ne)
        {
            foreach (var i in nodeErrors)
            {
                if (i == ne)
                {
                    nodeErrors.Remove(i);
                    return;
                }
            }
        }
    }
}
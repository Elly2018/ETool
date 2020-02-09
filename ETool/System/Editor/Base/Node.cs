using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ETool.ANode;

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
        public virtual void SelectionChanged(bool change) { }
        public virtual void DragChanged(bool change) { }
        #endregion

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
            GUIStyle ti = StyleUtility.GetStyle(StyleType.GUI_Title);
            ti.padding.left = 7;
            ti.padding.right = 7;
            GUI.Label(rect, EToolString.GetString_Node(EToolString.GetNodeTitle(GetType()), unlocalTitle), ti);

            for (int i = 0; i < fs.Count; i++)
            {
                /* Update field */
                fs[i].rect = new Rect(rect.x + 15f, rect.y + rect.height + (PropertiesHeight * i), rect.width - 30f, PropertiesHeight);
                fs[i].inPoint.rect = fs[i].rect;
                fs[i].outPoint.rect = fs[i].rect;

                fs[i].Draw();
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
            switch (e.type)
            {
                case EventType.MouseDown:
                    {
                        /* Right mouse */
                        if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                        {
                            ProcessContextMenu();
                            e.Use();
                            //isSelected = false;
                        }   

                        if (!e.shift && !NodeBasedEditor.Instance.IfAnyOtherNodeAreSelected(this) && !rect.Contains(e.mousePosition))
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
                            if (rect.Contains(e.mousePosition))
                            {
                                isDragged = true;
                                GUI.changed = true;
                                GUI.FocusControl(null);
                                isSelected = true;
                                SelectionChanged(isSelected);
                                DragChanged(isDragged);
                            }
                        }

                        isDragged = false;
                        DragChanged(isDragged);
                        break;
                    }

                case EventType.MouseDrag:
                    {
                        if (e.button == 0 && isSelected)
                        {
                            EditorUtility.SetDirty(NodeBasedEditor.Instance);
                            Drag(e.delta);
                            //e.Use();
                            return true;
                        }
                        break;
                    }
                    
            }
            return false;
        }

        /// <summary>
        /// When cursor is on node, and receive right click event
        /// </summary>
        public virtual void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Description"), false, OnClickDescription);

            if (NodeBasedEditor.Instance.CheckAnyNodeSelect())
            {
                genericMenu.AddItem(new GUIContent("Copy selection"), false, NodeBasedEditor.Instance.OnClickCopy);
            }

            if (NodeBasedEditor.Instance.CheckAnyConnectionSelect() || NodeBasedEditor.Instance.CheckAnyNodeSelect())
            {
                genericMenu.AddItem(new GUIContent("Delete Selected"), false, NodeBasedEditor.Instance.DeleteSelection);
            }

            if (nodeErrors.Count != 0)
            {
                foreach(var i in nodeErrors)
                {
                    genericMenu.AddItem(new GUIContent("ErrorMessage: " + i.errorType.ToString()), false, OnClickErrorMessage, i.errorString);
                }
            }

            if(NodeType == typeof(ACustomEventCall).FullName)
            {
                foreach(var i in NodeBasedEditor.Instance.GetAllCustomEventName())
                {
                    genericMenu.AddItem(new GUIContent("Change Event =>/" + i.addEventName), false, ChangeCustomEvent, i);
                }
                foreach(var i in NodeBasedEditor.Instance.GetAllInheritCustomEventName())
                {
                    genericMenu.AddItem(new GUIContent("Change Event =>/" + i), false, ChangeInheritCustomEvent, i);
                }
            }

            genericMenu.ShowAsContext();
        }

        protected void ChangeCustomEvent(object o)
        {
            AddCustomEvent target = (AddCustomEvent)o;
            NodeBase nb = (NodeBase)this;
            if (nb != null)
            {
                nb.unlocalTitle = target.addEventName;
                nb.targetPage = target.page;
            }
        }

        protected void ChangeInheritCustomEvent(object o)
        {
            string target = (string)o;
            NodeBase nb = (NodeBase)this;
            if (nb != null)
            {
                nb.unlocalTitle = target;
                nb.targetPage = 0;
            }   
        }

        /// <summary>
        /// User want to check error message
        /// </summary>
        /// <param name="messageString"></param>
        protected void OnClickErrorMessage(object messageString)
        {
            NodeBasedEditor.Instance.GreyBackgroundOkButton((string)messageString);
        }

        /// <summary>
        /// User want to check node description
        /// </summary>
        protected void OnClickDescription()
        {
            NodeBasedEditor.Instance.GreyBackgroundOkButton(EToolString.GetString_Node(EToolString.GetNodeDes(GetType()), ""));
        }
    }
}
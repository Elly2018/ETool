using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using ETool.ANode;
using System.Linq;

/// <summary>
/// The base of node editor window
/// This window content the basic function
/// </summary>
namespace ETool
{
    public class NodeBasedEditor : EditorWindow
    {
        /* Define connect point */
        private ConnectionPoint selectedInPoint = null;
        private ConnectionPoint selectedOutPoint = null;
        private GreyB GreyBackground;

        /* Select ablueprint */
        public EBlueprint selectBlueprint;
        private List<NodeBase> clipBorad = null;
        private int selectionPage;

        /* Define the left panel */
        private Vector2 sizeLimit;

        /* Define Background offset */
        private Vector2 offset;
        private Vector2 drag;

        /* Define local use variable for editing */
        private Type addNode;
        private Assembly assmbly;

        public static GUITheme uITheme;
        private static NodeBasedEditor NBE;
        public static NodeBasedEditor Instance
        {
            get
            {
                //Debug.LogWarning("Call Instance");
                if (NBE == null)
                    NBE = GetWindow<NodeBasedEditor>();
                return NBE;
            }
        }

        [MenuItem("Window/ETool/Component Node Editor")]
        public static void OpenWindow()
        {
            /* \\\uwu\\\ seens somebody is calling me hehe */
            NodeBasedEditor.NBE = GetWindow<NodeBasedEditor>();
            NodeBasedEditor.NBE.titleContent = new GUIContent("Component Node Editor");
        }

        private void OnEnable()
        {
            assmbly = Assembly.GetExecutingAssembly();
            if (selectBlueprint != null)
                selectBlueprint.nodes = EBlueprint.InitializeBlueprint(
                    selectBlueprint.nodes,
                    selectBlueprint.blueprintVariables,
                    selectBlueprint.blueprintEvent.customEvent);
        }

        private void OnGUI()
        {
            DrawBG();
            if (selectBlueprint != null)
            {
                PreventRepeatInstance();
                DrawNodes();
                DrawConnections();
                ProcessNodeEvents(Event.current);
            }
            ProcessEvents(Event.current);
            DrawMenuBar();
            StateCheck();
            if (GUI.changed) Repaint();
        }

        ///
        /// Drawing function collection
        ///
        #region Draw related
        private void DrawBG()
        {
            if(uITheme == GUITheme.Dark)
            {
                DrawColorBG(new Color(0, 0, 0, 0.75f));
                DrawGrid(20, 0.2f, Color.black);
                DrawGrid(100, 0.4f, Color.black);
            }
            if (uITheme == GUITheme.Light)
            {
                DrawColorBG(new Color(1, 1, 1, 0.85f));
                DrawGrid(20, 0.2f, Color.grey);
                DrawGrid(100, 0.4f, Color.grey);
            }
        }
        /// <summary>
        /// Drawing the top horizontal menu bar <br />
        /// it contain few buttons
        /// </summary>
        private void DrawMenuBar()
        {
            GUI.Box(new Rect(0, 0, position.width, 26), "");
            DrawMenuButtons(10.0f);
        }

        /// <summary>
        /// Drawing the top horizontal menu bar buttons <br />
        /// Each button will have offset
        /// </summary>
        private void DrawMenuButtons(float leftOffset)
        {
            sizeLimit = new Vector2(100.0f, 20.0f);
            int ButtonPadding = 1;

            if(selectBlueprint != null)
            {
                List<string> customNameList = new List<string>();
                customNameList.Add("Main Editor");
                customNameList.Add("Constructor");
                customNameList.Add("Physics");

                for (int i = 0; i < selectBlueprint.blueprintEvent.customEvent.Count; i++)
                {
                    customNameList.Add(selectBlueprint.blueprintEvent.customEvent[i].eventName);
                }
                EditorGUI.BeginChangeCheck();
                selectionPage = EditorGUI.Popup(GetMenuButtonRect(ButtonPadding, leftOffset, sizeLimit), selectionPage, customNameList.ToArray());
                if (selectionPage > customNameList.Count - 1) selectionPage = 0;
                if (EditorGUI.EndChangeCheck())
                    CenterViewer();
            }
            ButtonPadding++;
            uITheme = (GUITheme)EditorGUI.EnumPopup(GetMenuButtonRect(ButtonPadding, leftOffset, sizeLimit), uITheme);
            ButtonPadding++;
            if (GUI.Button(GetMenuButtonRect(ButtonPadding, leftOffset, sizeLimit), "Help"))
            {
                string message =
                    "Hotkey Map: \n\n" +
                    "Ctrl + C \t Center Page\n" +
                    "Ctrl + F \t Center Selected Nodes\n";
                GreyBackground = new GreyB() { Okbutton = true, Message = message };
            }
            ButtonPadding++;
            if (GUI.Button(GetMenuButtonRect(ButtonPadding, leftOffset, sizeLimit), "Cancel"))
            {
                selectBlueprint = null;
            }
        }

        /// <summary>
        /// Get the menu button rect
        /// </summary>
        /// <param name="index">Index of button</param>
        /// <param name="left">Left offset</param>
        /// <returns></returns>
        private Rect GetMenuButtonRect(int index, float left, Vector2 size)
        {
            return new Rect(left + (size.x * (index - 1)) + 3 * index, 3, size.x, size.y);
        }

        /// <summary>
        /// Drawing background grid
        /// </summary>
        /// <param name="gridSpacing">Grid distance between each line</param>
        /// <param name="gridOpacity">Grid color opacity</param>
        /// <param name="gridColor">grid color</param>
        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }

        /// <summary>
        /// Draw a box fullfill with whole window
        /// </summary>
        /// <param name="c"></param>
        private void DrawColorBG(Color c)
        {
            GUI.color = c;
            GUI.Box(new Rect(0, 0, position.width, position.height), new GUIContent());
            GUI.color = Color.white;
        }

        /// <summary>
        /// Drawing all node
        /// </summary>
        private void DrawNodes()
        {
            if (selectBlueprint.nodes != null)
            {
                for (int i = 0; i < selectBlueprint.nodes.Count; i++)
                {
                    NodeBase nb = (selectBlueprint.nodes[i] as NodeBase);
                    if (nb != null && nb.page == selectionPage)
                    {
                        nb.Draw();
                    }
                        
                }
            }
        }

        /// <summary>
        /// Drawing all connection
        /// </summary>
        private void DrawConnections()
        {
            for (int i = 0; i < selectBlueprint.connections.Count; i++)
            {
                if (selectBlueprint.connections[i].page == selectionPage)
                {
                    selectBlueprint.connections[i].Draw();
                }
            }
        }

        /// <summary>
        /// Drawing message grey background <br />
        /// It will effect the messageState
        /// </summary>
        private void DrawGreyBackground()
        {
            Texture2D greyImage = new Texture2D(1, 1);
            Color[] tColor = new Color[greyImage.width * greyImage.height];
            for (int i = 0; i < greyImage.width * greyImage.height; i++)
            {
                tColor[i] = new Color(0, 0, 0, 0.8f);
            }
            greyImage.SetPixels(0, 0, greyImage.width, greyImage.height, tColor);
            GUI.DrawTexture(new Rect(0, 0, position.width, position.height), greyImage);
        }

        /// <summary>
        /// After drawing message grey background <br />
        /// What text you want to put it in the center
        /// </summary>
        /// <param name="message">The message you want to put it center</param>
        private void GreyBackgroundCenterText(string message)
        {
            GUIStyle centerSkin = new GUIStyle();
            centerSkin.fontStyle = FontStyle.Bold;
            centerSkin.fontSize = 15;
            centerSkin.alignment = TextAnchor.MiddleCenter;
            centerSkin.normal.textColor = Color.white;
            GUI.Label(new Rect(0, 0, position.width, position.height), message, centerSkin);
        }

        /// <summary>
        /// After drawing message grey background <br />
        /// What text you want to put it in the center
        /// </summary>
        /// <param name="message">The message you want to put it center</param>
        /// <param name="messageColor">The message text color</param>
        private void GreyBackgroundCenterText(string message, Color messageColor)
        {
            GUIStyle centerSkin = new GUIStyle();
            centerSkin.fontStyle = FontStyle.Bold;
            centerSkin.fontSize = 15;
            centerSkin.alignment = TextAnchor.MiddleCenter;
            centerSkin.normal.textColor = messageColor;
            GUI.Label(new Rect(0, 0, position.width, position.height), message, centerSkin);
        }

        /// <summary>
        /// Craete a grey background instance <br />
        /// Background will active when variable is not null
        /// </summary>
        /// <param name="message"></param>
        public void GreyBackgroundOkButton(string message)
        {
            GreyBackground = new GreyB() { Message = message, Okbutton = true };
        }
        #endregion

        ///
        /// Event function collection
        ///
        #region Event Related
        /// <summary>
        /// Receive gui event <br />
        /// This function is editor related
        /// </summary>
        /// <param name="e">GUI event object</param>
        private void ProcessEvents(Event e)
        {
            EBlueprint target = Selection.activeObject as EBlueprint;
            if (target != null)
                selectBlueprint = target;

            if(GreyBackground == null)
            {
                drag = Vector2.zero;
                switch (e.type)
                {
                    case EventType.MouseDown:
                        if (e.button == 1)
                        {
                            ProcessContextMenu(e.mousePosition);
                        }
                        break;
                    case EventType.MouseDrag:
                        if (e.button == 2)
                        {
                            OnDrag(e.delta);
                        }
                        break;
                }
            }

            if (e.keyCode == KeyCode.Escape)
                ClearConnectionSelection();

            if (e.keyCode == KeyCode.C && e.control && e.type == EventType.KeyDown)
                CenterViewer();

            if (e.keyCode == KeyCode.F && e.control && e.type == EventType.KeyDown)
                CenterSelectionNodes();
        }

        /// <summary>
        /// Put the editor position offset back to zero
        /// </summary>
        private void CenterViewer()
        {
            Vector2 sumPos = Vector2.zero;
            int count = 0;
            foreach (var i in selectBlueprint.nodes)
            {
                if(i.page == selectionPage)
                {
                    sumPos += new Vector2(i.rect.position.x + i.rect.width / 2, i.rect.position.y + i.rect.height / 2);
                    count++;
                }
            }
            if (count == 0) return;
            sumPos /= count;
            OnDrag(-sumPos);
            OnDrag(new Vector2(position.width / 2, position.height / 2));
            offset = Vector2.zero;
        }

        /// <summary>
        /// Go to the center of selected nodes
        /// </summary>
        private void CenterSelectionNodes()
        {
            List<NodeBase> selectnode = new List<NodeBase>();
            foreach(var i in selectBlueprint.nodes)
            {
                if (i.page == selectionPage)
                {
                    if (i.isSelected) selectnode.Add(i);
                }
            }
            if (selectnode.Count == 0) return;

            Vector2 sumPos = Vector2.zero;
            foreach (var i in selectnode)
            {
                sumPos += new Vector2(i.rect.position.x + i.rect.width / 2, i.rect.position.y + i.rect.height / 2);
            }
            sumPos /= selectnode.Count;
            OnDrag(-sumPos);
            OnDrag(new Vector2(position.width / 2, position.height / 2));
            offset = Vector2.zero;
        }

        /// <summary>
        /// Receive gui event <br />
        /// Passing event to child node <br />
        /// This function is node and connection related
        /// </summary>
        /// <param name="e">GUI event object</param>
        private void ProcessNodeEvents(Event e)
        {
            bool ClickAnyNode = false;

            for (int i = selectBlueprint.nodes.Count - 1; i >= 0; i--)
            {
                if (selectBlueprint.nodes[i].rect.Contains(e.mousePosition) &&
                    e.type == EventType.MouseDown && e.button == 0)
                {
                    ClickAnyNode = true;
                }

                if(selectBlueprint.nodes[i].page == selectionPage)
                {
                    bool guiChanged = selectBlueprint.nodes[i].ProcessEvents(e);

                    if (guiChanged)
                    {
                        GUI.changed = true;
                    }
                }
            }

            if (!ClickAnyNode && e.type == EventType.MouseDown && e.button == 0) CleanNodeSelection();
        }

        /// <summary>
        /// Clean all node selection
        /// </summary>
        private void CleanNodeSelection()
        {
            for(int i = 0; i < selectBlueprint.nodes.Count; i++)
            {
                selectBlueprint.nodes[i].isSelected = false;
            }
        }

        /// <summary>
        /// Right click event in the editor
        /// </summary>
        /// <param name="mousePosition">Right click position</param>
        private void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            Type[] allTypes = assmbly.GetTypes();

            /* Sort by type name */
            List<Type> sorted = new List<Type>();
            var order = from e in allTypes orderby e.Name select e;
            foreach(var i in order)
            {
                sorted.Add(i);
            }
            allTypes = sorted.ToArray();

            /* Adding content into menu */
            for (int i = 0; i < allTypes.Length; i++)
            {
                if (allTypes[i].IsSubclassOf(typeof(Node)))
                {
                    NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                    if (nodePath != null)
                    {
                        addNode = allTypes[i];
                        genericMenu.AddItem(new GUIContent(nodePath.Path), false, OnClickAddNode, new AddClickEvent(mousePosition, allTypes[i]));
                    }
                }
            }

            /* Adding custom event into menu */
            for(int i = 0; i < selectBlueprint.blueprintEvent.customEvent.Count; i++)
            {
                genericMenu.AddItem(new GUIContent("Add Node/Custom Event/" + selectBlueprint.blueprintEvent.customEvent[i].eventName),
                    false, OnClickAddCustomEvent, new AddCustomEvent(mousePosition, selectBlueprint.blueprintEvent.customEvent[i].eventName, i + 2));
            }

            if (clipBorad != null)
                genericMenu.AddItem(new GUIContent("Paste"), false, OnClickPasteNodes, new PasteClickEvent(mousePosition));
            else
                genericMenu.AddDisabledItem(new GUIContent("Paste"));

            /* Draw content on screen */
            genericMenu.ShowAsContext();
        }

        /// <summary>
        /// Editor drag event <br />
        /// When drag the editor, trying to moving the viewer
        /// </summary>
        /// <param name="delta">Delta from original point</param>
        private void OnDrag(Vector2 delta)
        {
            drag = delta;

            if (selectBlueprint.nodes != null)
            {
                for (int i = 0; i < selectBlueprint.nodes.Count; i++)
                {
                    selectBlueprint.nodes[i].Drag(delta);
                }
            }

            GUI.changed = true;
        }

        /// <summary>
        /// When the right click menu active <br />
        /// And user is click add node <br />
        /// local variable: addnode will change to target
        /// </summary>
        /// <param name="addEvent">Target node (Strcut)AddClickEvent </param>
        private void OnClickAddNode(object addEvent)
        {
            AddClickEvent ace = (AddClickEvent)addEvent;
            addNode = ace.add;
            OnClickAddNode(ace.mousePosition);
        }

        private void OnClickAddCustomEvent(object addEvent)
        {
            AddCustomEvent ace = (AddCustomEvent)addEvent;
            addNode = typeof(ACustomEventCall);
            NodeBase n = OnClickAddNode(ace.mousePosition);
            ACustomEventCall nec = n as ACustomEventCall;
            if(nec != null)
            {
                nec.targetPage = ace.page;
                nec.title = ace.addEventName;
            }
        }
        /// <summary>
        /// When the right click menu active <br />
        /// And user is click add node <br />
        /// local variable: addnode will change to target
        /// </summary>
        /// <param name="addEvent">Target node (Strcut)AddClickEvent </param>
        private void OnClickAddNode(object addEvent, int page)
        {
            AddClickEvent ace = (AddClickEvent)addEvent;
            addNode = ace.add;
            OnClickAddNode(ace.mousePosition, page);
        }

        /// <summary>
        /// When the right click menu active <br />
        /// Create instance of target in this position on the grid
        /// </summary>
        /// <param name="mousePosition">Mouse pos</param>
        private NodeBase OnClickAddNode(Vector2 mousePosition)
        {
            /* Default */
            List<object> _args = new List<object>();
            _args.Add(mousePosition);
            _args.Add(200);
            _args.Add(60);

            NodeBase n = (NodeBase)assmbly.CreateInstance(addNode.FullName, false, BindingFlags.Public | BindingFlags.Instance, null, _args.ToArray(), null, null);
            selectBlueprint.nodes.Add(n);
            n.Initialize();
            n.page = selectionPage;
            return n;
        }

        /// <summary>
        /// When the right click menu active <br />
        /// Create instance of target in this position on the grid
        /// </summary>
        /// <param name="mousePosition">Mouse pos</param>
        private void OnClickAddNode(Vector2 mousePosition, int page)
        {
            /* Default */
            List<object> _args = new List<object>();
            _args.Add(mousePosition);
            _args.Add(200);
            _args.Add(60);

            NodeBase n = (NodeBase)assmbly.CreateInstance(addNode.FullName, false, BindingFlags.Public | BindingFlags.Instance, null, _args.ToArray(), null, null);
            selectBlueprint.nodes.Add(n);
            n.Initialize();
            n.page = page;
        }

        /// <summary>
        /// Delete selection nodes
        /// </summary>
        public void OnClickRemoveSelectionNode()
        {
            List<NodeBase> nb = new List<NodeBase>();
            foreach(var i in selectBlueprint.nodes)
            {
                if (i.isSelected) nb.Add(i);
            }
            foreach(var i in nb)
            {
                OnClickRemoveNode(i);
            }
        }

        /// <summary>
        /// This will send as delegate to all node <br />
        /// When node need to be delete <br />
        /// this event will called
        /// </summary>
        /// <param name="node">Target node</param>
        public void OnClickRemoveNode(NodeBase node)
        {
            List<Connection> removeList = new List<Connection>();
            int index = selectBlueprint.nodes.IndexOf(node);
            /* Delete related connection */
            for (int i = 0; i < selectBlueprint.connections.Count; i++)
            {
                if (selectBlueprint.nodes.IndexOf(node) == selectBlueprint.connections[i].inPointMark.x)
                {
                    removeList.Add(selectBlueprint.connections[i]);
                }

                if (selectBlueprint.nodes.IndexOf(node) == selectBlueprint.connections[i].outPointMark.x)
                {
                    removeList.Add(selectBlueprint.connections[i]);
                }
            }

            /* Delete connection list */
            foreach (var i in removeList)
            {
                OnClickRemoveConnection(i);
            }

            /* Delete node from node list */
            selectBlueprint.nodes.Remove(node);

            /* Shift connection */
            foreach(var i in selectBlueprint.connections)
            {
                if(i.inPointMark.x > index)
                {
                    i.inPointMark.x--;
                }
                if (i.outPointMark.x > index)
                {
                    i.outPointMark.x--;
                }
            }
        }

        /// <summary>
        /// This will only use in editor use
        /// </summary>
        /// <param name="nodes">Target node array</param>
        private void OnClickRemoveNode(NodeBase[] nodes)
        {
            foreach (var i in nodes) OnClickRemoveNode(i);
        }

        /// <summary>
        /// When click field in point
        /// </summary>
        /// <param name="inPoint"></param>
        public void OnClickInPoint(ConnectionPoint inPoint)
        {
            if(selectedInPoint != null)
                selectedInPoint.Selected = false;
            inPoint.Selected = true;
            selectedInPoint = inPoint;

            if (selectedOutPoint != null)
            {
                if (GetConnectionInfo(selectedOutPoint).x != GetConnectionInfo(selectedInPoint).x &&
                    !CheckConnectionExist(GetConnectionInfo(selectedInPoint), GetConnectionInfo(selectedOutPoint)))
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    Debug.LogWarning("Connection repeat");
                    ClearConnectionSelection();
                }
            }
        }

        /// <summary>
        /// Check the input connection type match anything in the list
        /// </summary>
        /// <param name="_in">Input mark</param>
        /// <param name="_out">Output mark</param>
        /// <returns></returns>
        private bool CheckConnectionExist(Vector2Int _in, Vector2 _out)
        {
            foreach(var i in selectBlueprint.connections)
            {
                if (i.inPointMark == _in && i.outPointMark == _out) return true;
            }
            return false;
        }

        public void OnClickOutPoint(ConnectionPoint outPoint)
        {
            if (selectedOutPoint != null)
                selectedOutPoint.Selected = false;
            outPoint.Selected = true;
            selectedOutPoint = outPoint;
        }

        public void OnClickRemoveConnection(Connection connection)
        {
            selectBlueprint.nodes[connection.inPointMark.x].fields[connection.inPointMark.y].onConnection = false;
            selectBlueprint.connections.Remove(connection);
        }

        public void OnClickCopyNodes()
        {
            List<NodeBase> nb = new List<NodeBase>();
            foreach(var i in selectBlueprint.nodes)
            {
                if (i.isSelected) nb.Add(EBlueprint.MakeInstanceNode(i, selectBlueprint.blueprintVariables, selectBlueprint.blueprintEvent.customEvent));
            }
            clipBorad = nb;
        }

        public void OnClickPasteNodes(object pasteEvent)
        {
            PasteClickEvent pasteClickEvent = (PasteClickEvent)pasteEvent;

        }

        private void CreateConnection()
        {
            Vector2Int _in = GetConnectionInfo(selectedInPoint);
            Vector2Int _out = GetConnectionInfo(selectedOutPoint);

            /* Field type check */
            if (selectBlueprint.nodes[_in.x].fields[_in.y].fieldType != selectBlueprint.nodes[_out.x].fields[_out.y].fieldType ||
                selectBlueprint.nodes[_in.x].fields[_in.y].fieldContainer != selectBlueprint.nodes[_out.x].fields[_out.y].fieldContainer)
            {
                Debug.LogWarning("Type mismatch");
                return;
            }

            foreach(var i in selectBlueprint.connections)
            {
                if(i.inPointMark == _in)
                {
                    Debug.LogWarning("Twice input detect");
                    return;
                }
            }

            Connection c = new Connection(_in, _out, false);
            c.page = selectionPage;

            /* Set field to connection */
            c.fieldType = selectBlueprint.nodes[_in.x].fields[_in.y].fieldType;

            /* Trigger on connection statt */
            selectBlueprint.nodes[_in.x].fields[_in.y].onConnection = true;

            selectBlueprint.connections.Add(c);
            
        }

        private void ClearConnectionSelection()
        {
            if(selectedInPoint != null)
                selectedInPoint.Selected = false;
            if (selectedOutPoint != null)
                selectedOutPoint.Selected = false;
            selectedInPoint = null;
            selectedOutPoint = null;
        }

        #endregion

        #region Editor Event

        private void PreventRepeatInstance()
        {
            List<Node> n = new List<Node>();
            for(int i = 0; i < selectBlueprint.nodes.Count; i++)
            {
                for (int k = i + 1; k < selectBlueprint.nodes.Count; k++)
                {
                    if(selectBlueprint.nodes[i] == selectBlueprint.nodes[k])
                    {
                        selectBlueprint.nodes.RemoveAt(k);
                    }
                }
            }
        }

        private void StateCheck()
        {
            if (selectBlueprint == null)
            {
                GreyBackground = new GreyB() { Message = "Please select blueprint", Okbutton = false };
            }
            else
            {
                if(GreyBackground != null)
                {
                    if(GreyBackground.Message == "Please select blueprint" && GreyBackground.Okbutton == false)
                    {
                        GreyBackground = null;
                    }
                }
            }
            if(selectBlueprint != null)
            {
                StateCheck_Main();
                StateCheck_CustomEvent();
            }
            if(GreyBackground != null)
            {
                DrawGreyBackground();
                if (GreyBackground.Okbutton)
                {
                    Vector2 buttonSize = new Vector2(100, 50);
                    if (GUI.Button(
                        new Rect( 
                            (position.width / 2) - (buttonSize.x / 2),
                            (position.height / 2) - (buttonSize.y / 2) + (position.height / 4),
                            buttonSize.x, buttonSize.y), "Ok"))
                    {
                        GreyBackground = null;
                    }
                }
                if(GreyBackground != null)
                    GreyBackgroundCenterText(GreyBackground.Message);
            }
        }

        private void StateCheck_Main()
        {
            /* Start */
            if (selectBlueprint.blueprintEvent.startEvent && !CheckEventNodeExist(EventNodeType.Start))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AStart)), 0);
            else if (!selectBlueprint.blueprintEvent.startEvent && CheckEventNodeExist(EventNodeType.Start))
                OnClickRemoveNode(GetEventNode(EventNodeType.Start));

            /* Update */
            if (selectBlueprint.blueprintEvent.updateEvent && !CheckEventNodeExist(EventNodeType.Update))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AUpdate)), 0);
            else if (!selectBlueprint.blueprintEvent.updateEvent && CheckEventNodeExist(EventNodeType.Update))
                OnClickRemoveNode(GetEventNode(EventNodeType.Update));

            /* FixedUpdate */
            if (selectBlueprint.blueprintEvent.fixedUpdateEvent && !CheckEventNodeExist(EventNodeType.FixedUpdate))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AFixedUpdate)), 0);
            else if (!selectBlueprint.blueprintEvent.fixedUpdateEvent && CheckEventNodeExist(EventNodeType.FixedUpdate))
                OnClickRemoveNode(GetEventNode(EventNodeType.FixedUpdate));

            /* OnDestory */
            if (selectBlueprint.blueprintEvent.onDestroyEvent && !CheckEventNodeExist(EventNodeType.OnDestory))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnDestory)), 0);
            else if (!selectBlueprint.blueprintEvent.onDestroyEvent && CheckEventNodeExist(EventNodeType.OnDestory))
                OnClickRemoveNode(GetEventNode(EventNodeType.OnDestory));

            /* Construct */
            if (!CheckEventNodeExist(EventNodeType.Constructor))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AConstructor)), 1);

            /* OnCollisionEnter */
            if (selectBlueprint.blueprintEvent.physicsEvent.onCollisionEnter && !CheckEventNodeExist(EventNodeType.onCollisionEnter))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnCollisionEnter)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onCollisionEnter && CheckEventNodeExist(EventNodeType.onCollisionEnter))
                OnClickRemoveNode(GetEventNode(EventNodeType.onCollisionEnter));

            /* OnCollisionExit */
            if (selectBlueprint.blueprintEvent.physicsEvent.onCollisionExit && !CheckEventNodeExist(EventNodeType.onCollisionExit))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnCollisionExit)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onCollisionExit && CheckEventNodeExist(EventNodeType.onCollisionExit))
                OnClickRemoveNode(GetEventNode(EventNodeType.onCollisionExit));

            /* OnCollisionStay */
            if (selectBlueprint.blueprintEvent.physicsEvent.onCollisionStay && !CheckEventNodeExist(EventNodeType.onCollisionStay))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnCollisionStay)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onCollisionStay && CheckEventNodeExist(EventNodeType.onCollisionStay))
                OnClickRemoveNode(GetEventNode(EventNodeType.onCollisionStay));

            /* OnDestory */
            if (selectBlueprint.blueprintEvent.onDestroyEvent && !CheckEventNodeExist(EventNodeType.OnDestory))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnDestory)), 0);
            else if (!selectBlueprint.blueprintEvent.onDestroyEvent && CheckEventNodeExist(EventNodeType.OnDestory))
                OnClickRemoveNode(GetEventNode(EventNodeType.OnDestory));

            /* OnDestory */
            if (selectBlueprint.blueprintEvent.onDestroyEvent && !CheckEventNodeExist(EventNodeType.OnDestory))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnDestory)), 0);
            else if (!selectBlueprint.blueprintEvent.onDestroyEvent && CheckEventNodeExist(EventNodeType.OnDestory))
                OnClickRemoveNode(GetEventNode(EventNodeType.OnDestory));

            /* OnDestory */
            if (selectBlueprint.blueprintEvent.onDestroyEvent && !CheckEventNodeExist(EventNodeType.OnDestory))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnDestory)), 0);
            else if (!selectBlueprint.blueprintEvent.onDestroyEvent && CheckEventNodeExist(EventNodeType.OnDestory))
                OnClickRemoveNode(GetEventNode(EventNodeType.OnDestory));
        }

        private void StateCheck_CustomEvent()
        {
            for (int i = 0; i < selectBlueprint.blueprintEvent.customEvent.Count; i++)
            {
                if (CheckCustomEventNodeExist(i + EBlueprint.DefaultPageCount))
                {
                    ACustomEvent customBuffer = GetCustomEventNode(i + EBlueprint.DefaultPageCount);
                    if (customBuffer.title != selectBlueprint.blueprintEvent.customEvent[i].eventName)
                        customBuffer.title = selectBlueprint.blueprintEvent.customEvent[i].eventName;
                    if (!ACustomEvent.CheckArugmentsMatch(selectBlueprint.blueprintEvent.customEvent[i].arugments, customBuffer))
                        customBuffer.DynamicFieldInitialize(new BlueprintInput(null, null, null, null, selectBlueprint.blueprintEvent.customEvent));
                }
                else
                {
                    OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(ACustomEvent)), i + EBlueprint.DefaultPageCount);
                }
            }

            for (int i = 0; i < selectBlueprint.blueprintEvent.customEvent.Count; i++)
            {
                NodeBase[] nb = GetAllCustomEventCallNode(i + EBlueprint.DefaultPageCount);
                foreach (var j in nb)
                {
                    if (j.title != selectBlueprint.blueprintEvent.customEvent[i].eventName)
                        j.title = selectBlueprint.blueprintEvent.customEvent[i].eventName;
                    if (!ACustomEvent.CheckArugmentsMatch(selectBlueprint.blueprintEvent.customEvent[i].arugments, j))
                        j.DynamicFieldInitialize(new BlueprintInput(null, null, null, null, selectBlueprint.blueprintEvent.customEvent));
                }
            }
        }

        private bool CheckCustomEventNodeExist(int page)
        {
            foreach(var i in selectBlueprint.nodes)
            {
                if(i.NodeType == typeof(ACustomEvent).FullName)
                {
                    if (i.page == page) return true;
                }
            }
            return false;
        }

        private NodeBase[] GetAllCustomEventCallNode(int page)
        {
            List<NodeBase> result = new List<NodeBase>();
            foreach (var i in selectBlueprint.nodes)
            {
                if (i.NodeType == typeof(ACustomEventCall).FullName)
                {
                    if (i.targetPage == page) result.Add(i);
                }
            }
            return result.ToArray();
        }

        private ACustomEvent GetCustomEventNode(int page)
        {
            foreach (var i in selectBlueprint.nodes)
            {
                if (i.NodeType == typeof(ACustomEvent).FullName)
                {
                    if (i.page == page) return i as ACustomEvent;
                }
            }
            return null;

        }

        private bool CheckEventNodeExist(EventNodeType event_node)
        {
            foreach (var i in selectBlueprint.nodes)
            {
                switch (event_node)
                {
                    case EventNodeType.Start:
                        if (i.NodeType == typeof(AStart).FullName) return true;
                        break;
                    case EventNodeType.Update:
                        if (i.NodeType == typeof(AUpdate).FullName) return true;
                        break;
                    case EventNodeType.FixedUpdate:
                        if (i.NodeType == typeof(AFixedUpdate).FullName) return true;
                        break;
                    case EventNodeType.Constructor:
                        if (i.NodeType == typeof(AConstructor).FullName) return true;
                        break;
                    case EventNodeType.OnDestory:
                        if (i.NodeType == typeof(AOnDestory).FullName) return true;
                        break;
                    case EventNodeType.onCollisionEnter:
                        if (i.NodeType == typeof(AOnCollisionEnter).FullName) return true;
                        break;
                    case EventNodeType.onCollisionExit:
                        if (i.NodeType == typeof(AOnCollisionExit).FullName) return true;
                        break;
                    case EventNodeType.onCollisionStay:
                        if (i.NodeType == typeof(AOnCollisionStay).FullName) return true;
                        break;
                }
            }
            return false;
        }

        public bool CheckNodeExist(NodeBase n)
        {
            foreach (var i in selectBlueprint.nodes)
            {
                if (i == n) return true;
            }
            return false;
        }

        private NodeBase[] GetEventNode(EventNodeType event_node)
        {
            List<NodeBase> result = new List<NodeBase>();
            foreach (var i in selectBlueprint.nodes)
            {
                switch (event_node)
                {
                    case EventNodeType.Start:
                        if (i.GetType() == typeof(AStart)) result.Add(i);
                        break;
                    case EventNodeType.Update:
                        if (i.GetType() == typeof(AUpdate)) result.Add(i);
                        break;
                    case EventNodeType.FixedUpdate:
                        if (i.GetType() == typeof(AFixedUpdate)) result.Add(i);
                        break;
                    case EventNodeType.Constructor:
                        if (i.GetType() == typeof(AConstructor)) result.Add(i);
                        break;
                    case EventNodeType.OnDestory:
                        if (i.GetType() == typeof(AOnDestory)) result.Add(i);
                        break;
                    case EventNodeType.onCollisionEnter:
                        if (i.GetType() == typeof(AOnCollisionEnter)) result.Add(i);
                        break;
                    case EventNodeType.onCollisionExit:
                        if (i.GetType() == typeof(AOnCollisionExit)) result.Add(i);
                        break;
                    case EventNodeType.onCollisionStay:
                        if (i.GetType() == typeof(AOnCollisionStay)) result.Add(i);
                        break;
                }
            }
            return result.ToArray();
        }

        private Vector2Int GetConnectionInfo(ConnectionPoint c)
        {
            Vector2Int result = Vector2Int.zero;
            for(int i = 0; i < selectBlueprint.nodes.Count; i++)
            {
                for (int j = 0; j < selectBlueprint.nodes[i].fields.Count; j++)
                {
                    if (selectBlueprint.nodes[i].fields[j].inPoint == c || selectBlueprint.nodes[i].fields[j].outPoint == c) return new Vector2Int(i, j);
                }
            }
            return result;
        }

        public ConnectionPoint GetConnectionPoint(Vector2Int mark, bool input)
        {
            if (input)
                return selectBlueprint.nodes[mark.x].fields[mark.y].inPoint;
            else
                return selectBlueprint.nodes[mark.x].fields[mark.y].outPoint;
        }

        public int GetSelectionNodeCount()
        {
            int result = 0;
            foreach(var i in selectBlueprint.nodes)
            {
                if (i.isSelected) result++;
            }
            return result;
        }

        public Node GetNodeByField(Field field)
        {
            foreach(var i in selectBlueprint.nodes)
            {
                foreach(var j in i.fields)
                {
                    if (j == field) return i;
                }
            }
            return null;
        }

        public List<BlueprintVariable> GetAllCustomVariable()
        {
            return selectBlueprint.blueprintVariables;
        }

        public void RemoveRelateConnectionInField(Field field)
        {
            List<Connection> removeConnection = new List<Connection>();
            if(field.connectionType == ConnectionType.DataBoth || field.connectionType == ConnectionType.EventBoth)
            {
                Vector2Int t = GetConnectionInfo(field.inPoint);
                foreach (var i in selectBlueprint.connections)
                {
                    if (i.inPointMark == t) removeConnection.Add(i);
                }
                t = GetConnectionInfo(field.outPoint);
                foreach (var i in selectBlueprint.connections)
                {
                    if (i.outPointMark == t) removeConnection.Add(i);
                }
            }else if (field.connectionType == ConnectionType.DataInput || field.connectionType == ConnectionType.EventInput)
            {
                Vector2Int t = GetConnectionInfo(field.inPoint);
                foreach (var i in selectBlueprint.connections)
                {
                    if (i.inPointMark == t) removeConnection.Add(i);
                }
            }
            else if (field.connectionType == ConnectionType.DataOutput || field.connectionType == ConnectionType.EventOutput)
            {
                Vector2Int t = GetConnectionInfo(field.outPoint);
                foreach (var i in selectBlueprint.connections)
                {
                    if (i.outPointMark == t) removeConnection.Add(i);
                }
            }

            foreach(var i in removeConnection)
            {
                OnClickRemoveConnection(i);
            }
        }
        #endregion
    }

    public struct AddClickEvent
    {
        public Vector2 mousePosition;
        public Type add;

        public AddClickEvent(Vector2 mousePosition, Type add)
        {
            this.mousePosition = mousePosition;
            this.add = add;
        }
    }

    public struct AddCustomEvent
    {
        public Vector2 mousePosition;
        public string addEventName;
        public int page;

        public AddCustomEvent(Vector2 mousePosition, string addEventName, int page)
        {
            this.mousePosition = mousePosition;
            this.addEventName = addEventName;
            this.page = page;
        }
    }

    public struct PasteClickEvent
    {
        public Vector2 mousePosition;

        public PasteClickEvent(Vector2 mousePosition)
        {
            this.mousePosition = mousePosition;
        }
    }

    public enum EventNodeType
    {
        Start,
        Update,
        FixedUpdate,
        OnDestory,
        Constructor,

        onCollisionEnter,
        onCollisionExit,
        onCollisionStay,

        onCollisionEnter2D,
        onCollisionExit2D,
        onCollisionStay2D,

        onTriggerEnter,
        onTriggerExit,
        onTriggerStay,

        onTriggerEnter2D,
        onTriggerExit2D,
        onTriggerStay2D
    }

    public enum GUITheme
    {
        Light, Dark
    }

    public class GreyB
    {
        public bool Okbutton;
        public string Message;
    }
}
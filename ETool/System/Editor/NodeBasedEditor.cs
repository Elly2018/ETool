using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using ETool.ANode;
using System.Linq;
using UnityEditor.SceneManagement;

/// <summary>
/// The base of node editor window
/// This window content the basic function
/// </summary>
namespace ETool
{
    public class NodeBasedEditor : EditorWindow
    {
        #region Variable
        /// <summary>
        /// Define select input point
        /// </summary>
        private ConnectionPoint selectedInPoint = null;

        /// <summary>
        /// Define select output point
        /// </summary>
        private ConnectionPoint selectedOutPoint = null;

        /// <summary>
        /// Define background data <br />
        /// Use for show message in screen
        /// </summary>
        private GreyBackground GreyBackground;

        /// <summary>
        /// Editor select blueprint
        /// </summary>
        public EBlueprint selectBlueprint;

        /// <summary>
        /// Define clipboard nodes and connection
        /// </summary>
        private Clipborad clipBorad = null;

        /// <summary>
        /// Define current page selected index
        /// </summary>
        private int selectionPage;

        /// <summary>
        /// Define top menu button size
        /// </summary>
        private Vector2 sizeLimit;

        /// <summary>
        /// Define the zoom
        /// </summary>
        private int zoomLevel;

        /// <summary>
        /// Define background grid offset
        /// </summary>
        private Vector2 offset;

        /// <summary>
        /// Define background grid drag delta
        /// </summary>
        private Vector2 drag;

        /// <summary>
        /// Define the position buffer <br />
        /// When user is using search menu
        /// </summary>
        private Vector2 searchPosition;

        /// <summary>
        /// Define the scroll view position
        /// </summary>
        private Vector2 searchScrollPosition;

        /// <summary>
        /// Define search struct
        /// </summary>
        private SearchStruct searchStruct = null;

        /// <summary>
        /// Define how many search element are
        /// </summary>
        private int searchElementCount;

        /// <summary>
        /// Define local use variable for adding node buffer
        /// </summary>
        private Type addNode;

        /// <summary>
        /// Define current execute assembly
        /// </summary>
        private Assembly assmbly;

        /// <summary>
        /// Define what corner pop message is
        /// </summary>
        private string popMessage;

        /// <summary>
        /// Define all nodebase type
        /// </summary>
        private Type[] allTypes;

        /// <summary>
        /// Define GUI theme
        /// </summary>
        public static GUITheme uITheme;

        /// <summary>
        /// Define singleton node base editor
        /// </summary>
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
        #endregion

        /// <summary>
        /// When user open node window
        /// </summary>
        [MenuItem("Window/ETool/Component Node Editor")]
        public static void OpenWindow()
        {
            /* \\\uwu\\\ seens somebody is calling me hehe */
            NodeBasedEditor.NBE = GetWindow<NodeBasedEditor>();
            NodeBasedEditor.NBE.titleContent = new GUIContent("Component Node Editor");
        }

        /// <summary>
        /// When GUI enable <br />
        /// Initialize content
        /// </summary>
        private void OnEnable()
        {
            InitalizeContent();
        }

        private void OnFocus()
        {
            InitalizeContent();
        }

        private void InitalizeContent()
        {
            assmbly = Assembly.GetExecutingAssembly();
            if (selectBlueprint != null)
                selectBlueprint.nodes = EBlueprint.InitializeBlueprint(
                    selectBlueprint.nodes,
                    selectBlueprint.blueprintVariables,
                    selectBlueprint.blueprintEvent.customEvent);
            StyleUtility.Initialize();
        }

        /// <summary>
        /// This will called when gui is using
        /// </summary>
        private void OnGUI()
        {
            DrawBG();
            if (selectBlueprint != null)
            {
                PreventRepeatInstance();
                DrawConnections();
                DrawNodes();
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
            sizeLimit = new Vector2(100.0f, 20.0f);
            DrawMenuButtons(10.0f);
            DrawMenuZooming(10.0f);
        }

        /// <summary>
        /// Zoom level range is 0 - 5
        /// </summary>
        /// <param name="rightOffset">Button Right Offset</param>
        private void DrawMenuZooming(float rightOffset)
        {
            if (GUI.Button(new Rect(position.width - rightOffset - (sizeLimit.x / 3), 4f, (sizeLimit.x / 3), sizeLimit.y), "+"))
            {
                if(zoomLevel < 5)
                {
                    zoomLevel++;
                }
            }
            if (GUI.Button(new Rect(position.width - rightOffset - ((sizeLimit.x / 3) * 2) - 5f, 4f, (sizeLimit.x / 3), sizeLimit.y), "-"))
            {
                if(zoomLevel > 0)
                {
                    zoomLevel--;
                }
            }
            GUI.Label(new Rect(position.width - rightOffset - ((sizeLimit.x / 3) * 4) - 10f, 4f, (sizeLimit.x / 3) * 2, sizeLimit.y), "Zoom: " + zoomLevel.ToString());
        }

        /// <summary>
        /// Drawing the top horizontal menu bar buttons <br />
        /// Each button will have offset
        /// </summary>
        private void DrawMenuButtons(float leftOffset)
        {
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
                    "Shift + C \t\t Center Page\n" +
                    "F \t\t Center Selected Nodes\n" +
                    "Ctrl + C \t\t Copy Selection \n" +
                    "Ctrl + V \t\t Paste Clipboard \n" +
                    "Delete \t\t Delete Selection \n";
                GreyBackground = new GreyBackground() { Okbutton = true, Message = message };
            }
            ButtonPadding++;
            if (GUI.Button(GetMenuButtonRect(ButtonPadding, leftOffset, sizeLimit), "Refresh"))
            {
                InitalizeContent();
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
                    selectBlueprint.connections[i].ProcessEvents(Event.current);
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
            centerSkin.alignment = TextAnchor.MiddleLeft;
            centerSkin.padding.left = (int)(position.width / 3);
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
            GreyBackground = new GreyBackground() { Message = message, Okbutton = true };
        }

        /// <summary>
        /// When search struct is not null <br />
        /// GUI will freeze all movement <br />
        /// User can only click button or select element or enter inputfield text
        /// </summary>
        public void DrawSearchMenu()
        {
            float borderGap = 50;
            float fieldWidth = 15;

            Rect inputFieldTextRect = new Rect(borderGap, 70, position.width - (borderGap * 2), fieldWidth);
            Rect inputFieldRect = new Rect(borderGap, 100, position.width - (borderGap * 2), fieldWidth);
            Rect selectButton = new Rect(borderGap, position.height - 100, position.width / 2 - borderGap * 2, fieldWidth * 3);
            Rect cancelButton = new Rect(position.width / 2 + borderGap, position.height - 100, position.width - (position.width / 2 + borderGap * 2), fieldWidth * 3);
            Rect scrollRect = new Rect(borderGap, 200, position.width - borderGap * 2, position.height - 400);

            GUIStyle SearchMenuTitle = new GUIStyle();
            SearchMenuTitle.alignment = TextAnchor.MiddleCenter;
            SearchMenuTitle.richText = true;
            SearchMenuTitle.fontStyle = FontStyle.Bold;
            SearchMenuTitle.fontSize = 13;

            GUIStyle SelectedNode = new GUIStyle();
            SelectedNode.fontStyle = FontStyle.Bold;
            SelectedNode.richText = true;

            GUIStyle UnSelectedNode = new GUIStyle();
            UnSelectedNode.fontStyle = FontStyle.Normal;
            UnSelectedNode.richText = true;

            EditorGUI.LabelField(inputFieldTextRect, "<color=white>Input Field</color>", SearchMenuTitle);
            EditorGUI.BeginChangeCheck();
            searchStruct.inputField = EditorGUI.TextField(inputFieldRect, searchStruct.inputField);
            if (EditorGUI.EndChangeCheck())
            {
                searchScrollPosition = Vector2.zero;
            }

            searchElementCount = GetCurrentSearchElement(allTypes, searchStruct.inputField);
            searchScrollPosition = GUI.BeginScrollView(scrollRect, searchScrollPosition, new Rect(0, 0, scrollRect.width - scrollRect.x, searchElementCount * 20), false, true);
            for(int i = 0; i < allTypes.Length; i++)
            {
                NodePath np = allTypes[i].GetCustomAttribute<NodePath>();
                if(np != null)
                {
                    if (!np.Path.ToUpper().Contains(searchStruct.inputField.ToUpper()) && searchStruct.inputField.Length != 0) continue;

                    GUILayout.BeginHorizontal();

                    if(allTypes[i] == searchStruct.type)
                    {
                        EditorGUILayout.LabelField("<color=yellow>" + np.Path + "</color>", SelectedNode);
                    }
                    else
                    {
                        EditorGUILayout.LabelField("<color=white>" + np.Path + "</color>", UnSelectedNode);
                    }
                    if (GUILayout.Button("Select Me"))
                    {
                        searchStruct.type = allTypes[i];
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUI.EndScrollView();

            GUI.enabled = searchStruct.type != null;
            if (GUI.Button(selectButton, "Select"))
            {
                OnClickAddNode(new AddClickEvent(searchPosition, searchStruct.type), selectionPage);
                searchStruct = null;
            }
            GUI.enabled = true;

            if (GUI.Button(cancelButton, "Cancel"))
            {
                searchStruct = null;
            }
        }

        /// <summary>
        /// Drawing pop message in the corner
        /// </summary>
        private void DrawPopMessage()
        {
            Rect messageBG = new Rect(50, position.height - (position.height / 6), position.width - 100, 40);
            Rect messageButton = new Rect(position.width - 100 - 100, position.height - (position.height / 6) + 5, 80, 40 - 10);
            GUIStyle center = new GUIStyle();
            center.alignment = TextAnchor.MiddleCenter;
            center.normal.textColor = Color.yellow;
            center.fontSize = 14;
            center.normal.background = new Texture2D(1, 1);
            center.normal.background.SetPixel(0, 0, new Color(0.15f, 0.15f, 0.35f, 0.45f));
            center.normal.background.Apply();
            GUI.Box(messageBG, popMessage, center);
            if (GUI.Button(messageButton, "OK"))
            {
                popMessage = "";
            }
        }

        public void ShowPopMessage(string message)
        {
            popMessage = message;
        }

        private int GetCurrentSearchElement(Type[] alltype, string searchString)
        {
            int Result = 0;
            for (int i = 0; i < allTypes.Length; i++)
            {
                NodePath np = allTypes[i].GetCustomAttribute<NodePath>();
                if (np != null)
                {
                    if (!np.Path.ToUpper().Contains(searchString.ToUpper())) continue;
                    Result++;
                }
            }
            return Result;
        }
        #endregion

        ///
        /// Event function collection
        ///
        #region Event Related

        ///
        /// Event (editor)
        ///
        #region Editor Event
        /// <summary>
        /// Receive gui event <br />
        /// This function is editor related
        /// </summary>
        /// <param name="e">GUI event object</param>
        private void ProcessEvents(Event e)
        {
            if (searchStruct != null) return;
            EBlueprint target = Selection.activeObject as EBlueprint;
            GameObject targetN = Selection.activeObject as GameObject;

            if (target != null)
            {
                if(selectBlueprint == null || target != selectBlueprint)
                {
                    selectBlueprint = target;
                    InitalizeContent();
                    GUI.changed = true;
                }
            }
            if (targetN != null)
            {
                if (selectBlueprint == null || targetN.GetComponent<NodeComponent>() != null)
                {
                    if (targetN.GetComponent<NodeComponent>().ABlueprint != null)
                    {
                        if (targetN.GetComponent<NodeComponent>().ABlueprint != selectBlueprint)
                        {
                            selectBlueprint = targetN.GetComponent<NodeComponent>().ABlueprint;
                            InitalizeContent();
                            GUI.changed = true;
                        }
                    }
                }
            }

            if (GreyBackground == null)
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

            if (e.keyCode == KeyCode.C && e.shift && e.type == EventType.KeyDown)
                CenterViewer();

            if (e.keyCode == KeyCode.F && e.type == EventType.KeyDown)
                CenterSelectionNodes();

            if (e.keyCode == KeyCode.C && e.control && e.type == EventType.KeyDown)
            {
                OnClickCopy();
                GUI.changed = true;
            }

            if (e.keyCode == KeyCode.V && e.control && e.type == EventType.KeyDown)
            {
                OnClickPasteNodes(new PasteClickEvent() { mousePosition = e.mousePosition });
                GUI.changed = true;
            }
                
            if (e.keyCode == KeyCode.Delete && e.type == EventType.KeyDown)
            {
                DeleteSelection();
                GUI.changed = true;
            }
        }

        /// <summary>
        /// Receive gui event <br />
        /// Passing event to child node <br />
        /// This function is node and connection related
        /// </summary>
        /// <param name="e">GUI event object</param>
        private void ProcessNodeEvents(Event e)
        {
            if (searchStruct != null) return;
            bool ClickAnyNode = false;

            for (int i = 0; i < selectBlueprint.nodes.Count; i++)
            {
                if (selectBlueprint.nodes[i].rect.Contains(e.mousePosition) &&
                    e.type == EventType.MouseDown && e.button == 0)
                {
                    ClickAnyNode = true;
                }

                if (selectBlueprint.nodes[i].page == selectionPage)
                {
                    bool guiChanged = selectBlueprint.nodes[i].ProcessEvents(e);

                    if (guiChanged)
                    {
                        GUI.changed = true;
                    }
                }
            }

            if (!ClickAnyNode && e.type == EventType.MouseDown && e.button == 0) 
            {
                CleanNodeSelection();
                GUI.FocusControl(null);
            }
        }

        /// <summary>
        /// Right click event in the editor
        /// </summary>
        /// <param name="mousePosition">Right click position</param>
        private void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

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

            /* Adding search button into menu */
            genericMenu.AddItem(new GUIContent("Search Node"), false, OnClickSearchNode, mousePosition);

            /* Adding custom event into menu */
            for (int i = 0; i < selectBlueprint.blueprintEvent.customEvent.Count; i++)
            {
                genericMenu.AddItem(new GUIContent("Add Node/Custom Event/" + selectBlueprint.blueprintEvent.customEvent[i].eventName),
                    false, OnClickAddCustomEvent, new AddCustomEvent(mousePosition, selectBlueprint.blueprintEvent.customEvent[i], selectBlueprint.blueprintEvent.customEvent[i].eventName, i + EBlueprint.DefaultPageCount));
            }

            /* Adding inherit custom event into menu */
            if (selectBlueprint.Inherit != null)
            {
                List<BlueprintCustomEvent> buffer = selectBlueprint.Inherit.blueprintEvent.customEvent;
                for (int i = 0; i < buffer.Count; i++)
                {
                    genericMenu.AddItem(new GUIContent("Add Node/Custom Event/" + buffer[i].eventName),
                        false, OnClickAddCustomEvent, new AddCustomEvent(mousePosition, buffer[i], buffer[i].eventName, 0));
                }
            }

            /* Adding edit button */
            if (CheckAnyConnectionSelect() || CheckAnyNodeSelect())
            {
                genericMenu.AddItem(new GUIContent("Copy Selected"), false, OnClickCopy);
                genericMenu.AddItem(new GUIContent("Delete Selected"), false, DeleteSelection);
            }
            if (CheckAnyNodeSelect())
            {
                genericMenu.AddItem(new GUIContent("Delete Selected Node"), false, OnClickRemoveSelectionNode);
            }
            if (CheckAnyConnectionSelect())
            {
                genericMenu.AddItem(new GUIContent("Delete Selected Connection"), false, OnDeleteSelectedConnection);
            }

            /* Paste command */
            if (clipBorad != null)
                genericMenu.AddItem(new GUIContent("Paste"), false, OnClickPasteNodes, new PasteClickEvent(mousePosition));
            else
                genericMenu.AddDisabledItem(new GUIContent("Paste"));

            /* Draw content on screen */
            genericMenu.ShowAsContext();
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
        /// Clean all node selection
        /// </summary>
        private void CleanNodeSelection()
        {
            for(int i = 0; i < selectBlueprint.nodes.Count; i++)
            {
                selectBlueprint.nodes[i].isSelected = false;
            }
        }
        #endregion

        ///
        /// Event (state check)
        ///
        #region Event State Check
        /// <summary>
        /// Check if any other node are selected <br />
        /// This is use for multiple node drag
        /// </summary>
        /// <param name="node">Source node</param>
        /// <returns></returns>
        public bool IfAnyOtherNodeAreSelected(Node node)
        {
            foreach (var i in selectBlueprint.nodes)
            {
                if (i != node && i.isSelected) return true;
            }
            return false;
        }

        /// <summary>
        /// Check if any other connection is selected <br />
        /// This will effect the right click menu
        /// </summary>
        /// <returns></returns>
        public bool CheckAnyConnectionSelect()
        {
            foreach(var i in selectBlueprint.connections)
            {
                if (i.isSelected) return true;
            }
            return false;
        }

        /// <summary>
        /// Check if any other node is selected <br />
        /// This will effect the right click menu
        /// </summary>
        /// <returns></returns>
        public bool CheckAnyNodeSelect()
        {
            foreach (var i in selectBlueprint.nodes)
            {
                if (i.isSelected) return true;
            }
            return false;
        }

        /// <summary>
        /// Check the input connection type match anything in the list
        /// </summary>
        /// <param name="_in">Input mark</param>
        /// <param name="_out">Output mark</param>
        /// <returns></returns>
        private bool CheckConnectionExist(Vector2Int _in, Vector2 _out)
        {
            foreach (var i in selectBlueprint.connections)
            {
                if (i.inPointMark == _in && i.outPointMark == _out) return true;
            }
            return false;
        }
        #endregion

        ///
        /// Event (on click)
        ///
        #region On Click Event
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
        /// Pop up search menu <br />
        /// For user to search the node it want
        /// </summary>
        private void OnClickSearchNode(object pos)
        {
            searchPosition = (Vector2)pos;
            searchStruct = new SearchStruct() { inputField = "", type = null };
        }

        #region Node Related Event
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
                nec.unlocalTitle = ace.addEventName;
                nec.SetCustomEvent(ace.bce);
            }
        }
        /// <summary>
        /// When the right click menu active <br />
        /// And user is click add node <br />
        /// local variable: addnode will change to target
        /// </summary>
        /// <param name="addEvent">Target node (Strcut)AddClickEvent </param>
        private NodeBase OnClickAddNode(object addEvent, int page)
        {
            AddClickEvent ace = (AddClickEvent)addEvent;
            addNode = ace.add;
            return OnClickAddNode(ace.mousePosition, page);
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
            n.PostFieldInitialize();
            n.page = selectionPage;
            EditorUtility.SetDirty(selectBlueprint);
            return n;
        }

        /// <summary>
        /// When the right click menu active <br />
        /// Create instance of target in this position on the grid
        /// </summary>
        /// <param name="mousePosition">Mouse pos</param>
        private NodeBase OnClickAddNode(Vector2 mousePosition, int page)
        {
            /* Default */
            List<object> _args = new List<object>();
            _args.Add(mousePosition);
            _args.Add(200);
            _args.Add(60);

            NodeBase n = (NodeBase)assmbly.CreateInstance(addNode.FullName, false, BindingFlags.Public | BindingFlags.Instance, null, _args.ToArray(), null, null);
            selectBlueprint.nodes.Add(n);
            n.Initialize();
            n.PostFieldInitialize();
            n.page = page;
            EditorUtility.SetDirty(selectBlueprint);
            return n;
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
            EditorUtility.SetDirty(selectBlueprint);
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
            EditorUtility.SetDirty(selectBlueprint);
        }

        public void OnClickCopy()
        {
            List<NodeBase> nb = new List<NodeBase>();
            List<Connection> c = new List<Connection>();
            List<NodeBase> buffer = new List<NodeBase>();

            for (int i = 0; i < selectBlueprint.nodes.Count; i++)
            {
                if (selectBlueprint.nodes[i].isSelected)
                {
                    nb.Add(EBlueprint.MakeInstanceNode(selectBlueprint.nodes[i], selectBlueprint.blueprintVariables, selectBlueprint.blueprintEvent.customEvent));
                    buffer.Add(selectBlueprint.nodes[i]);
                }
            }

            for (int i = 0; i < selectBlueprint.connections.Count; i++)
            {
                if (selectBlueprint.connections[i].isSelected)
                {
                    if (buffer.Contains(selectBlueprint.nodes[selectBlueprint.connections[i].inPointMark.x]) &&
                            buffer.Contains(selectBlueprint.nodes[selectBlueprint.connections[i].outPointMark.x]))
                    {
                        Vector2Int In_P = new Vector2Int(buffer.IndexOf(selectBlueprint.nodes[selectBlueprint.connections[i].inPointMark.x]), selectBlueprint.connections[i].inPointMark.y);
                        Vector2Int Out_P = new Vector2Int(buffer.IndexOf(selectBlueprint.nodes[selectBlueprint.connections[i].outPointMark.x]), selectBlueprint.connections[i].outPointMark.y);
                        Connection bufferc = new Connection(In_P, Out_P, false);
                        bufferc.fieldType = selectBlueprint.connections[i].fieldType;
                        c.Add(bufferc);
                    }
                }
            }

            clipBorad = new Clipborad();
            clipBorad.nodeBases = nb;
            clipBorad.connections = c;
        }

        public void OnClickPasteNodes(object pasteEvent)
        {
            PasteClickEvent pasteClickEvent = (PasteClickEvent)pasteEvent;
            List<NodeBase> buffer = new List<NodeBase>();

            float max_x = -9999;
            float max_y = -9999;
            float min_x = 9999;
            float min_y = 9999;

            foreach (var i in clipBorad.nodeBases)
            {
                if (i.rect.position.x > max_x) max_x = i.rect.position.x;
                if (i.rect.position.y > max_y) max_y = i.rect.position.y;

                if (i.rect.position.x < min_x) min_x = i.rect.position.x;
                if (i.rect.position.y < min_y) min_y = i.rect.position.y;
            }

            Vector2 OgMin = new Vector2(min_x, min_y);
            Vector2 OgMax = new Vector2(max_x, max_y);
            Vector2 OgcenterP = new Vector2((max_x + min_x) / 2, (min_y + max_y) / 2);
            Vector2 Diff = pasteClickEvent.mousePosition - OgcenterP;

            /* Spawn nodes */
            for (int i = 0; i < clipBorad.nodeBases.Count; i++)
            {
                AddClickEvent a = new AddClickEvent() { add = Type.GetType(clipBorad.nodeBases[i].NodeType), mousePosition = clipBorad.nodeBases[i].rect.position + Diff };
                NodeBase n = OnClickAddNode(a, selectionPage);
                for(int j = 0; j < n.fields.Count; j++)
                {
                    n.fields[j].target = new GenericObject(clipBorad.nodeBases[i].fields[j].target);
                }
                n.DynamicFieldInitialize(null);
                buffer.Add(n);
                n.isSelected = true;
            }

            /* Spawn connection */
            for(int i = 0; i < clipBorad.connections.Count; i++)
            {
                NodeBase in_node = buffer[clipBorad.connections[i].inPointMark.x];
                NodeBase out_node = buffer[clipBorad.connections[i].outPointMark.x];
                Vector2Int In_P = new Vector2Int(selectBlueprint.nodes.IndexOf(in_node), clipBorad.connections[i].inPointMark.y);
                Vector2Int Out_P = new Vector2Int(selectBlueprint.nodes.IndexOf(out_node), clipBorad.connections[i].outPointMark.y);
                Connection bufferc = new Connection(In_P, Out_P, false);
                selectBlueprint.connections.Add(bufferc);
                bufferc.fieldType = clipBorad.connections[i].fieldType;
                bufferc.page = selectionPage;
                bufferc.isSelected = true;
            }

        }

        private void OnDeleteSelectedConnection()
        {
            for (int i = 0; i < selectBlueprint.connections.Count; i++)
            {
                if (selectBlueprint.connections[i].isSelected)
                {
                    OnClickRemoveConnection(selectBlueprint.connections[i]);
                    i--;
                }
            }
        }
        #endregion

        #region Connection Related Event
        private void CreateConnection()
        {
            Vector2Int _in = GetConnectionInfo(selectedInPoint);
            Vector2Int _out = GetConnectionInfo(selectedOutPoint);

            /* Field type check */
            if (selectBlueprint.nodes[_in.x].fields[_in.y].fieldType != selectBlueprint.nodes[_out.x].fields[_out.y].fieldType)
            {
                NodeBasedEditor.Instance.ShowPopMessage("Type mismatch");
                Debug.LogWarning("Type mismatch");
                return;
            }

            if (selectBlueprint.nodes[_in.x].fields[_in.y].fieldContainer != selectBlueprint.nodes[_out.x].fields[_out.y].fieldContainer)
            {
                NodeBasedEditor.Instance.ShowPopMessage("Container mismatch");
                Debug.LogWarning("Container mismatch");
                return;
            }

            foreach (var i in selectBlueprint.connections)
            {
                if (i.inPointMark == _in)
                {
                    NodeBasedEditor.Instance.ShowPopMessage("Twice input detect");
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
            EditorUtility.SetDirty(selectBlueprint);
        }

        /// <summary>
        /// Delete selection nodes
        /// </summary>
        private void OnClickRemoveSelectionNode()
        {
            List<NodeBase> nb = new List<NodeBase>();
            foreach (var i in selectBlueprint.nodes)
            {
                if (i.isSelected) nb.Add(i);
            }
            foreach (var i in nb)
            {
                OnClickRemoveNode(i);
            }
        }

        public void DeleteSelection()
        {
            OnDeleteSelectedConnection();
            OnClickRemoveSelectionNode();
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
        #endregion
        #endregion

        #region Editor State

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
            if (allTypes == null)
            {
                allTypes = GetAllNodebaseType();
            }
            if (selectBlueprint == null)
            {
                GreyBackground = new GreyBackground() { Message = "Please select blueprint", Okbutton = false };
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
            if(popMessage.Length != 0)
            {
                DrawPopMessage();
            }
            if(selectBlueprint != null)
            {
                StateCheck_Main();
                StateCheck_CustomEvent();
                StateCheck_MulEvent();
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
            if(searchStruct != null)
            {
                DrawGreyBackground();
                DrawSearchMenu();
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

            /* OnCollisionEnter2D */
            if (selectBlueprint.blueprintEvent.physicsEvent.onCollisionEnter2D && !CheckEventNodeExist(EventNodeType.onCollisionEnter2D))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnCollisionEnter2D)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onCollisionEnter2D && CheckEventNodeExist(EventNodeType.onCollisionEnter2D))
                OnClickRemoveNode(GetEventNode(EventNodeType.onCollisionEnter2D));

            /* OnCollisionExit2D */
            if (selectBlueprint.blueprintEvent.physicsEvent.onCollisionExit2D && !CheckEventNodeExist(EventNodeType.onCollisionExit2D))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnCollisionExit2D)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onCollisionExit2D && CheckEventNodeExist(EventNodeType.onCollisionExit2D))
                OnClickRemoveNode(GetEventNode(EventNodeType.onCollisionExit2D));

            /* OnCollisionStay2D */
            if (selectBlueprint.blueprintEvent.physicsEvent.onCollisionStay2D && !CheckEventNodeExist(EventNodeType.onCollisionStay2D))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnCollisionStay2D)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onCollisionStay2D && CheckEventNodeExist(EventNodeType.onCollisionStay2D))
                OnClickRemoveNode(GetEventNode(EventNodeType.onCollisionStay2D));

            /* OnTriggerEnter */
            if (selectBlueprint.blueprintEvent.physicsEvent.onTriggerEnter && !CheckEventNodeExist(EventNodeType.onTriggerEnter))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnTriggerEnter)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onTriggerEnter && CheckEventNodeExist(EventNodeType.onTriggerEnter))
                OnClickRemoveNode(GetEventNode(EventNodeType.onTriggerEnter));

            /* OnTriggerExit */
            if (selectBlueprint.blueprintEvent.physicsEvent.onTriggerExit && !CheckEventNodeExist(EventNodeType.onTriggerExit))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnTriggerExit)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onTriggerExit && CheckEventNodeExist(EventNodeType.onTriggerExit))
                OnClickRemoveNode(GetEventNode(EventNodeType.onTriggerExit));

            /* OnTriggerStay */
            if (selectBlueprint.blueprintEvent.physicsEvent.onTriggerStay && !CheckEventNodeExist(EventNodeType.onTriggerStay))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnTriggerStay)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onTriggerStay && CheckEventNodeExist(EventNodeType.onTriggerStay))
                OnClickRemoveNode(GetEventNode(EventNodeType.onTriggerStay));

            /* OnTriggerEnter2D */
            if (selectBlueprint.blueprintEvent.physicsEvent.onTriggerEnter2D && !CheckEventNodeExist(EventNodeType.onTriggerEnter2D))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnTriggerEnter2D)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onTriggerEnter2D && CheckEventNodeExist(EventNodeType.onTriggerEnter2D))
                OnClickRemoveNode(GetEventNode(EventNodeType.onTriggerEnter2D));

            /* OnTriggerExit2D */
            if (selectBlueprint.blueprintEvent.physicsEvent.onTriggerExit2D && !CheckEventNodeExist(EventNodeType.onTriggerExit2D))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnTriggerExit2D)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onTriggerExit2D && CheckEventNodeExist(EventNodeType.onTriggerExit2D))
                OnClickRemoveNode(GetEventNode(EventNodeType.onTriggerExit2D));

            /* OnTriggerStay2D */
            if (selectBlueprint.blueprintEvent.physicsEvent.onTriggerStay2D && !CheckEventNodeExist(EventNodeType.onTriggerStay2D))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnTriggerStay2D)), 2);
            else if (!selectBlueprint.blueprintEvent.physicsEvent.onTriggerStay2D && CheckEventNodeExist(EventNodeType.onTriggerStay2D))
                OnClickRemoveNode(GetEventNode(EventNodeType.onTriggerStay2D));

            /* OnDestory */
            if (selectBlueprint.blueprintEvent.onDestroyEvent && !CheckEventNodeExist(EventNodeType.OnDestory))
                OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(AOnDestory)), 0);
            else if (!selectBlueprint.blueprintEvent.onDestroyEvent && CheckEventNodeExist(EventNodeType.OnDestory))
                OnClickRemoveNode(GetEventNode(EventNodeType.OnDestory));
        }

        private void StateCheck_CustomEvent()
        {
            /* Buffer */
            List<ACustomEvent> buffer_ce = new List<ACustomEvent>();
            List<ACustomEventCall> buffer_cec = new List<ACustomEventCall>();

            /* Get all custom event related nodes */
            foreach (var i in selectBlueprint.nodes)
            {
                if(i.GetType() == typeof(ACustomEvent))
                {
                    buffer_ce.Add(i as ACustomEvent);
                }

                if (i.GetType() == typeof(ACustomEventCall))
                {
                    buffer_cec.Add(i as ACustomEventCall);
                }
            }

            foreach (var i in buffer_ce)
            {
                if(i.page >= EBlueprint.DefaultPageCount)
                {
                    /* Handle private */
                    foreach(var j in selectBlueprint.blueprintEvent.customEvent)
                    {
                        if(i.page == selectBlueprint.blueprintEvent.customEvent.IndexOf(j) + EBlueprint.DefaultPageCount)
                        {
                            i.unlocalTitle = j.eventName;
                        }
                    }
                }
            }
            foreach (var i in buffer_cec)
            {
                if (i.targetPage >= EBlueprint.DefaultPageCount)
                {
                    /* Handle private */
                }
                else
                {
                    /* Handle inherit */
                    bool exist = false;
                    bool errorexist = false;
                    NodeError ne = null;
                    foreach(var j in selectBlueprint.GetInheritEvent())
                    {
                        if (j.eventName == i.unlocalTitle) exist = true;
                    }
                    foreach (var j in i.nodeErrors)
                    {
                        if (j.errorType == NodeErrorType.Custom_Event_Does_Not_Exist)
                        {
                            errorexist = true;
                            ne = j;
                        }
                    }
                    if (exist && errorexist)
                    {
                        i.nodeErrors.Remove(ne);
                    }
                    if(!exist && !errorexist)
                    {
                        i.nodeErrors.Add(new NodeError() { errorType = NodeErrorType.Custom_Event_Does_Not_Exist, errorString = "The event name can not be found" });
                    }
                }
            }


            /* Pervent private node disappear */
            for (int i = 0; i < selectBlueprint.blueprintEvent.customEvent.Count; i++)
            {
                if (!CheckCustomEventNodeExist(i + EBlueprint.DefaultPageCount))
                {
                    NodeBase n = OnClickAddNode(new AddClickEvent(new Vector2(position.width / 2, position.height / 2), typeof(ACustomEvent)), i + EBlueprint.DefaultPageCount);
                    n.unlocalTitle = selectBlueprint.blueprintEvent.customEvent[i].eventName;
                    (n as ACustomEvent).SetCustomEvent(selectBlueprint.blueprintEvent.customEvent[i]);
                }
            }
        }

        private void StateCheck_MulEvent()
        {

        }

        #region Getter
        public AddCustomEvent[] GetAllCustomEventName()
        {
            List<AddCustomEvent> result = new List<AddCustomEvent>();
            for(int i = 0; i < selectBlueprint.blueprintEvent.customEvent.Count; i++)
            {
                result.Add(new AddCustomEvent(Vector2.zero, selectBlueprint.blueprintEvent.customEvent[i], selectBlueprint.blueprintEvent.customEvent[i].eventName, i + EBlueprint.DefaultPageCount));
            }
            return result.ToArray();
        }

        public string[] GetAllInheritCustomEventName()
        {
            List<string> result = new List<string>();
            foreach (var i in selectBlueprint.GetInheritEvent())
            {
                if(!selectBlueprint.blueprintEvent.customEvent.Contains(i))
                    result.Add(i.eventName);
            }
            return result.ToArray();
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

        private Type[] GetAllNodebaseType()
        {
            Type[] allTypes = assmbly.GetTypes();
            List<ForNodeNameSort> search = new List<ForNodeNameSort>();
            foreach (var i in allTypes)
            {
                NodePath np = i.GetCustomAttribute<NodePath>();
                if (np != null && i.GetCustomAttribute<NodeHide>() == null)
                    search.Add(new ForNodeNameSort() { type = i, nodepath = np.Path });
            }

            /* Sort by type name */
            List<Type> sorted = new List<Type>();
            var order = from e in search orderby e.nodepath select e;
            foreach (var i in order)
            {
                sorted.Add(i.type);
            }
            allTypes = sorted.ToArray();
            return allTypes;
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
                    case EventNodeType.onCollisionEnter2D:
                        if (i.NodeType == typeof(AOnCollisionEnter2D).FullName) return true;
                        break;
                    case EventNodeType.onCollisionExit2D:
                        if (i.NodeType == typeof(AOnCollisionExit2D).FullName) return true;
                        break;
                    case EventNodeType.onCollisionStay2D:
                        if (i.NodeType == typeof(AOnCollisionStay2D).FullName) return true;
                        break;
                    case EventNodeType.onTriggerEnter:
                        if (i.NodeType == typeof(AOnTriggerEnter).FullName) return true;
                        break;
                    case EventNodeType.onTriggerExit:
                        if (i.NodeType == typeof(AOnTriggerExit).FullName) return true;
                        break;
                    case EventNodeType.onTriggerStay:
                        if (i.NodeType == typeof(AOnTriggerStay).FullName) return true;
                        break;
                    case EventNodeType.onTriggerEnter2D:
                        if (i.NodeType == typeof(AOnTriggerEnter2D).FullName) return true;
                        break;
                    case EventNodeType.onTriggerExit2D:
                        if (i.NodeType == typeof(AOnTriggerExit2D).FullName) return true;
                        break;
                    case EventNodeType.onTriggerStay2D:
                        if (i.NodeType == typeof(AOnTriggerStay2D).FullName) return true;
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
                    case EventNodeType.onCollisionEnter2D:
                        if (i.GetType() == typeof(AOnCollisionEnter2D)) result.Add(i);
                        break;
                    case EventNodeType.onCollisionExit2D:
                        if (i.GetType() == typeof(AOnCollisionExit2D)) result.Add(i);
                        break;
                    case EventNodeType.onCollisionStay2D:
                        if (i.GetType() == typeof(AOnCollisionStay2D)) result.Add(i);
                        break;
                    case EventNodeType.onTriggerEnter:
                        if (i.GetType() == typeof(AOnTriggerEnter)) result.Add(i);
                        break;
                    case EventNodeType.onTriggerExit:
                        if (i.GetType() == typeof(AOnTriggerExit)) result.Add(i);
                        break;
                    case EventNodeType.onTriggerStay:
                        if (i.GetType() == typeof(AOnTriggerStay)) result.Add(i);
                        break;
                    case EventNodeType.onTriggerEnter2D:
                        if (i.GetType() == typeof(AOnTriggerEnter2D)) result.Add(i);
                        break;
                    case EventNodeType.onTriggerExit2D:
                        if (i.GetType() == typeof(AOnTriggerExit2D)) result.Add(i);
                        break;
                    case EventNodeType.onTriggerStay2D:
                        if (i.GetType() == typeof(AOnTriggerStay2D)) result.Add(i);
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
        public BlueprintCustomEvent bce;
        public string addEventName;
        public int page;

        public AddCustomEvent(Vector2 mousePosition, BlueprintCustomEvent bce, string addEventName, int page)
        {
            this.mousePosition = mousePosition;
            this.bce = bce;
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
        Dark, Light
    }

    public class GreyBackground
    {
        public bool Okbutton;
        public string Message;
    }

    public class SearchStruct
    {
        public Type type;
        public string inputField;
        public string[] nodeBasesSearch;
    }

    public class ForNodeNameSort
    {
        public Type type;
        public string nodepath;
    }

    public class Clipborad
    {
        public List<NodeBase> nodeBases;
        public List<Connection> connections;
    }
}
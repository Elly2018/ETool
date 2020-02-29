#if UNITY_EDITOR
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

/// <summary>
/// The base of node editor window
/// This window content the basic function
/// </summary>
namespace ETool
{
    public class NodeBasedEditor : EditorWindow
    {
        ///
        /// Variables
        ///
        #region Variable

        #region Selection
        private bool boxSelectionMode = false;
        private bool boxIsDeleteSelection = false;
        private bool boxIsSelecting = false;
        private Vector2 boxSelectionV1Point;
        private Vector2 boxSelectionV2Point;

        private ConnectionPoint[] preSelectionInPoint = new ConnectionPoint[1];
        private bool preSelectionChangeMode = false;
        private bool preSelectionChangeMode_Input = false;
        private ConnectionPoint[] preSelectionOutPoint = new ConnectionPoint[0];

        /// <summary>
        /// Define select input point
        /// </summary>
        private ConnectionPoint selectedInPoint = null;

        /// <summary>
        /// Define select output point
        /// </summary>
        private ConnectionPoint selectedOutPoint = null;
        #endregion

        #region Utility
        /// <summary>
        /// Define background data <br />
        /// Use for show message in screen
        /// </summary>
        private NodeEditorMessagePopup GreyBackground;

        /// <summary>
        /// Define clipboard nodes and connection
        /// </summary>
        private Clipborad clipBorad = null;

        /// <summary>
        /// Define search struct
        /// </summary>
        private SearchStruct searchStruct = null;

        /// <summary>
        /// Define type list struct
        /// </summary>
        private TypeListStruct typeListStruct = null;
        #endregion

        #region Editor
        /// <summary>
        /// Editor select blueprint
        /// </summary>
        public EBlueprint selectBlueprint;

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
        private float zoomLevel;

        /// <summary>
        /// Each key input how many gap will add on to editor
        /// </summary>
        private const float zoomGap = 0.05f;
        #endregion

        #region Buffer
        /// <summary>
        /// Define background grid offset
        /// </summary>
        private Vector2 offset;

        /// <summary>
        /// Define background grid drag delta
        /// </summary>
        private Vector2 drag;

        /// <summary>
        /// The page scroll position
        /// </summary>
        private Vector2 pageScrollPosition = new Vector2(0, 0);

        /// <summary>
        /// Define the position buffer <br />
        /// When user is using search menu
        /// </summary>
        private Vector2 searchPosition;

        /// <summary>
        /// Define the scroll view position of search view
        /// </summary>
        private Vector2 searchScrollPosition;

        /// <summary>
        /// Define the position buffer <br />
        /// When user is using type list menu
        /// </summary>
        private Vector2 typeListPosition;

        /// <summary>
        /// Define the scroll view position of type list view
        /// </summary>
        private Vector2 typeListScrollPosition;

        /// <summary>
        /// Define how many search element are
        /// </summary>
        private int searchElementCount;

        /// <summary>
        /// Define local use variable for adding node buffer
        /// </summary>
        private Type addNode;

        /// <summary>
        /// Define what corner pop message is
        /// </summary>
        private string popMessage;

        /// <summary>
        /// Define all nodebase type
        /// </summary>
        private Type[] allTypes;
        #endregion

        #region Singleton Target
        /// <summary>
        /// Define singleton node base editor
        /// </summary>
        private static NodeBasedEditor NBE;

        /// <summary>
        /// [Warning] <br />
        /// Please do not call this property at running time <br />
        /// It will trying to open the node editor gui
        /// </summary>
        public static EBlueprint Instance
        {
            get
            {
                //Debug.LogWarning("Call Instance");
                if (NBE == null)
                    NBE = GetWindow<NodeBasedEditor>();
                return NBE.selectBlueprint;
            }
        }

        /// <summary>
        /// [Warning] <br />
        /// Please do not call this property at running time <br />
        /// It will trying to open the node editor gui
        /// </summary>
        public static NodeBasedEditor Editor_Instance
        {
            get
            {
                if (NBE == null)
                    NBE = GetWindow<NodeBasedEditor>();
                return NBE;
            }
        }
        #endregion

        #endregion


        /// 
        /// Editor Window Event
        /// 
        #region GUI Event
        /// <summary>
        /// When user open node window
        /// </summary>
        [MenuItem("ETool/Component Node Editor")]
        public static void OpenWindow()
        {
            /* \\\uwu\\\ seens somebody is calling me hehe */
            NodeBasedEditor.NBE = GetWindow<NodeBasedEditor>();
            NodeBasedEditor.NBE.titleContent = new GUIContent("Component Node Editor");
            NodeBasedEditor.NBE.zoomLevel = 1;
        }

        /// <summary>
        /// When GUI enable <br />
        /// Initialize content
        /// </summary>
        private void OnEnable()
        {
            InitalizeContent();
        }

        /// <summary>
        /// When user click the editor window
        /// </summary>
        private void OnFocus()
        {
            InitalizeContent();
            selectedInPoint = null;
            selectedOutPoint = null;
            preSelectionChangeMode = false;
            preSelectionOutPoint = new ConnectionPoint[0];
            preSelectionInPoint = new ConnectionPoint[0];

            if (selectBlueprint != null)
                selectBlueprint.Connection_CleanConnectionPointSelection();
        }

        /// <summary>
        /// Initialize the editor window <br />
        /// This action including initialize the node and blueprint selection
        /// </summary>
        private void InitalizeContent()
        {
            if (selectBlueprint != null)
            {
                selectBlueprint.nodes = EBlueprint.InitializeBlueprint(
                    selectBlueprint.nodes,
                    selectBlueprint.blueprintVariables,
                    selectBlueprint.blueprintEvent.customEvent,
                    selectBlueprint.Inherit
                    );

                if(NBE != null)
                {
                    foreach(var i in selectBlueprint.nodes)
                    {
                        i.ConnectionUpdate();
                    }
                }
            }
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
        #endregion


        /// 
        /// Drawing function collection
        /// 
        #region Draw related
        private void DrawBG()
        {
            DrawColorBG(new Color(0, 0, 0, 0.75f));
            DrawGrid(20, 0.2f, Color.black);
            DrawGrid(100, 0.4f, Color.black);
        }
        /// <summary>
        /// Drawing the top horizontal menu bar <br />
        /// it contain few buttons
        /// </summary>
        private void DrawMenuBar()
        {
            GUI.Box(new Rect(0, 0, position.width, 60), "");
            sizeLimit = new Vector2(100.0f, 20.0f);
            DrawMenuButtons(10.0f);
            DrawMenuZooming(10.0f);
            DrawPageList();
        }

        /// <summary>
        /// Drawing the page list at the bottom of the menu bar
        /// </summary>
        private void DrawPageList()
        {
            List<Tuple<string, float>> customNameList = new List<Tuple<string, float>>();
            float TotalWidth = 0;
            if (selectBlueprint != null)
            {
                customNameList.Add(new Tuple<string, float>("Main Editor", "Main Editor".Length));
                customNameList.Add(new Tuple<string, float>("Constructor", "Constructor".Length));
                customNameList.Add(new Tuple<string, float>("Physics", "Physics".Length));

                for (int i = 0; i < selectBlueprint.blueprintEvent.customEvent.Count; i++)
                {
                    string n = selectBlueprint.blueprintEvent.customEvent[i].eventName;
                    customNameList.Add(new Tuple<string, float>(n, n.Length));
                }

                foreach(var i in customNameList)
                {
                    TotalWidth += i.Item2;
                }
            }

            Rect range = new Rect(0, 30, position.width, 60 - 26);
            pageScrollPosition = GUI.BeginScrollView(range, pageScrollPosition, new Rect(0, 26, TotalWidth * 16, 60 - 26));
            GUILayout.BeginArea(new Rect(0, 30, TotalWidth * 16, 60 - 26));
            GUILayout.BeginHorizontal();
            for (int i = 0; i < customNameList.Count; i++)
            {
                if (GUILayout.Button(customNameList[i].Item1, GUILayout.Width(customNameList[i].Item2 * 15)))
                {
                    selectionPage = i;
                    CenterViewer();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            GUI.EndScrollView();
        }

        /// <summary>
        /// Zoom level range is 0 - 5
        /// </summary>
        /// <param name="rightOffset">Button Right Offset</param>
        private void DrawMenuZooming(float rightOffset)
        {
            zoomLevel = GUI.HorizontalSlider(new Rect(position.width - rightOffset - ((sizeLimit.x / 3) * 2) - 5f, 4f, (sizeLimit.x / 2), sizeLimit.y), zoomLevel, GetZoomLevel().minimum, GetZoomLevel().maximum);
            GUI.Label(new Rect(position.width - rightOffset - ((sizeLimit.x / 3) * 5) + 10f, 4f, (sizeLimit.x / 3) * 3, sizeLimit.y), "Zoom: " + zoomLevel.ToString("0.00"));

            if (zoomLevel < GetZoomLevel().minimum) zoomLevel = GetZoomLevel().minimum;
            if (zoomLevel > GetZoomLevel().maximum) zoomLevel = GetZoomLevel().maximum;
        }

        /// <summary>
        /// Drawing the top horizontal menu bar buttons <br />
        /// Each button will have offset
        /// </summary>
        private void DrawMenuButtons(float leftOffset)
        {
            int ButtonPadding = 1;

            if (GUI.Button(GetMenuButtonRect(ButtonPadding, leftOffset, sizeLimit), "Help (F1)"))
            {
                GreyBackground = GetHelpMessagePopup();
            }

            if(selectBlueprint != null)
                GUI.Box(RectGetMenuCenterRect(400f), selectBlueprint.name);
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
        private void DrawGreyBackgroundCenterText(string message)
        {
            GUIStyle centerSkin = new GUIStyle();
            centerSkin.fontStyle = FontStyle.Bold;
            centerSkin.fontSize = 15;
            centerSkin.alignment = TextAnchor.MiddleLeft;
            centerSkin.padding.left = (int)(position.width / 2.5f);
            centerSkin.normal.textColor = Color.white;
            GUI.Label(new Rect(0, 0, position.width, position.height), message, centerSkin);
        }

        /// <summary>
        /// After drawing message grey background <br />
        /// What text you want to put it in the center
        /// </summary>
        /// <param name="message">The message you want to put it center</param>
        /// <param name="messageColor">The message text color</param>
        private void DrawGreyBackgroundCenterText(string message, Color messageColor)
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
        public void DrawGreyBackgroundOkButton(string message)
        {
            GreyBackground = new NodeEditorMessagePopup() { Message = message, Okbutton = true };
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
                Instance.Node_AddNode(searchPosition, selectionPage, searchStruct.type);
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

        /// <summary>
        /// Show pop message on the gui bottom screen
        /// </summary>
        /// <param name="message"></param>
        public void ShowPopMessage(string message)
        {
            popMessage = message;
        }

        private void DrawTypeList()
        {
            float borderGap = 50;
            float fieldWidth = 15;

            Rect buttonRect = new Rect(borderGap, 70, position.width - (borderGap * 2), fieldWidth * 3);
            Rect scrollRect = new Rect(borderGap, 200, position.width - borderGap * 2, position.height - 400);
            Rect selectButton = new Rect(borderGap, position.height - 100, position.width / 2 - borderGap * 2, fieldWidth * 3);
            Rect cancelButton = new Rect(position.width / 2 + borderGap, position.height - 100, position.width - (position.width / 2 + borderGap * 2), fieldWidth * 3);

            GUIStyle buttonselect = new GUIStyle("button");
            GUIStyle buttonunselect = new GUIStyle("button");
            buttonselect.fontStyle = FontStyle.Bold;

            GUILayout.BeginArea(buttonRect);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Basic Type", GUILayout.Height(25)))
            {
                typeListStruct.typeListTypeSelection = 1;
            }
            if (GUILayout.Button("Unity Type", GUILayout.Height(25)))
            {
                typeListStruct.typeListTypeSelection = 2;
            }
            if (GUILayout.Button("Component Type", GUILayout.Height(25)))
            {
                typeListStruct.typeListTypeSelection = 3;
            }
            if (GUILayout.Button("Enum Type", GUILayout.Height(25)))
            {
                typeListStruct.typeListTypeSelection = 4;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

            EnumUseStruct[] enumUseStructs = null;
            switch (typeListStruct.typeListTypeSelection)
            {
                case 1:
                    enumUseStructs = Field.GetFieldTypeEnumUseStruct(10, 49);
                    break;
                case 2:
                    enumUseStructs = Field.GetFieldTypeEnumUseStruct(50, 199);
                    break;
                case 3:
                    enumUseStructs = Field.GetFieldTypeEnumUseStruct(200, 1999);
                    break;
                case 4:
                    enumUseStructs = Field.GetFieldTypeEnumUseStruct(2000, 99999);
                    break;
            }

            if (enumUseStructs != null)
            {
                typeListPosition = GUI.BeginScrollView(scrollRect, typeListPosition, new Rect(0, 0, scrollRect.width - scrollRect.x, enumUseStructs.Length * 25));
                if (typeListStruct.typeListTypeSelection != 0)
                {
                    for (int i = 0; i < enumUseStructs.Length; i++)
                    {
                        if(enumUseStructs[i].fieldIndex == (int)typeListStruct.fieldSelection)
                        {
                            if (GUILayout.Button(enumUseStructs[i].fieldName, buttonselect))
                            {
                                typeListStruct.fieldSelection = (FieldType)enumUseStructs[i].fieldIndex;
                            }
                        }
                        else
                        {
                            if (GUILayout.Button(enumUseStructs[i].fieldName, buttonunselect))
                            {
                                typeListStruct.fieldSelection = (FieldType)enumUseStructs[i].fieldIndex;
                            }
                        }
                    }
                }
                GUI.EndScrollView();
            }

            GUI.enabled = typeListStruct.fieldSelection != FieldType.Event;
            if (GUI.Button(selectButton, "Select"))
            {
                typeListStruct.target.target.genericBasicType.target_Int = (int)typeListStruct.fieldSelection;
                typeListStruct.target.FieldUpdate();
                typeListStruct = null;
            }
            GUI.enabled = true;

            if (GUI.Button(cancelButton, "Cancel"))
            {
                typeListStruct = null;
            }
        }

        private void DrawPreConnection(Event e)
        {
            if (!preSelectionChangeMode)
            {
                if (preSelectionOutPoint.Length > 0)
                {
                    foreach (var i in preSelectionOutPoint)
                    {
                        Field f = selectBlueprint.GetFieldByConnectionPoint(i);

                        /* Drawing bezier curve */
                        Handles.DrawBezier(
                            e.mousePosition, // Start point
                            i.rect.center,  // End point
                            e.mousePosition + Vector2.left * 150f, // Start tangent
                            i.rect.center - Vector2.left * 150f, // End tangent
                            Field.GetColorByFieldType(f.fieldType, 1.0f), // Color
                            null, // Texture
                            5f
                            );
                        GUI.changed = true;
                    }
                }
            }
            else if (preSelectionChangeMode && !preSelectionChangeMode_Input)
            {
                foreach (var i in preSelectionInPoint)
                {
                    Field f = selectBlueprint.GetFieldByConnectionPoint(i);

                    /* Drawing bezier curve */
                    Handles.DrawBezier(
                        i.rect.center, // Start point
                        e.mousePosition,  // End point
                        i.rect.center + Vector2.left * 150f, // Start tangent
                        e.mousePosition - Vector2.left * 150f, // End tangent
                        Field.GetColorByFieldType(f.fieldType, 1.0f), // Color
                        null, // Texture
                        5f
                        );
                    GUI.changed = true;
                }
            }
            else if (preSelectionChangeMode && preSelectionChangeMode_Input)
            {
                foreach (var i in preSelectionOutPoint)
                {
                    Field f = selectBlueprint.GetFieldByConnectionPoint(i);

                    /* Drawing bezier curve */
                    Handles.DrawBezier(
                        e.mousePosition, // Start point
                        i.rect.center,  // End point
                        e.mousePosition + Vector2.left * 150f, // Start tangent
                        i.rect.center - Vector2.left * 150f, // End tangent
                        Field.GetColorByFieldType(f.fieldType, 1.0f), // Color
                        null, // Texture
                        5f
                        );
                    GUI.changed = true;
                }
            }
        }

        public void PreConnectionPoint(ConnectionPoint cp)
        {
            preSelectionOutPoint = new ConnectionPoint[1] { cp };
        }

        #region Draw Getter
        /// <summary>
        /// It will make the help message popup instance and return <br />
        /// </summary>
        /// <returns>Help Popup</returns>
        private NodeEditorMessagePopup GetHelpMessagePopup()
        {
            string message =
                    "Hotkey Map: \n\n" +
                    "S \t\t Self Menu\n" +
                    "C \t\t Constant Menu\n" +
                    "T \t\t GameObject Menu\n" +
                    "E \t\t Component Menu\n" +
                    "L \t\t Logic Menu\n" +
                    "M \t\t Math Menu\n" +
                    "V \t\t Casting Menu\n\n" +

                    "Shift + C \t\t Center Page\n" +
                    "F \t\t Center Selected Nodes\n" +
                    "Ctrl + C \t\t Copy Selection \n" +
                    "Ctrl + V \t\t Paste Clipboard \n" +
                    "Delete \t\t Delete Selection \n\n" +

                    "[ \t\t Zoom Out \n" +
                    "] \t\t Zoom In \n" +
                    "A \t\t Select All \n" +
                    "B \t\t Box Selection \n";
            return new NodeEditorMessagePopup() { Okbutton = true, Message = message };
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
        /// The method will help draw the box which contain blueprint name <br />
        /// The box will in the center of the menubar
        /// </summary>
        /// <param name="width">How long does the width is</param>
        /// <returns></returns>
        private Rect RectGetMenuCenterRect(float width)
        {
            return new Rect((position.width - width) / 2, 3, width, 20);
        }

        /// <summary>
        /// Get how many element that target string relate have <br />
        /// It will return the count of the type that have target string in their nodepath
        /// </summary>
        /// <param name="alltype">All Type</param>
        /// <param name="searchString">Target Search String</param>
        /// <returns></returns>
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
        #endregion


        /// 
        /// Event function collection
        /// 
        #region Event Related

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
                if (selectBlueprint == null && targetN.GetComponent<ENodeComponent>() != null)
                {
                    if (targetN.GetComponent<ENodeComponent>().ABlueprint != null)
                    {
                        if (targetN.GetComponent<ENodeComponent>().ABlueprint != selectBlueprint)
                        {
                            selectBlueprint = targetN.GetComponent<ENodeComponent>().ABlueprint;
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
                            if (!boxSelectionMode)
                            {
                                OnDrag(e.delta * (1 / GetZoomLevel().ratio));
                            }
                        }
                        break;
                }
            }

            if (e.keyCode == KeyCode.T && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
                ProcessContextTransformMenu(e.mousePosition);

            if (e.keyCode == KeyCode.E && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
                ProcessContextComponentMenu(e.mousePosition);

            if (e.keyCode == KeyCode.W && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
                ProcessContextEToolMenu(e.mousePosition);

            if (e.keyCode == KeyCode.I && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
                ProcessContextInputMenu(e.mousePosition);

            if (e.keyCode == KeyCode.M && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
                ProcessContextMathMenu(e.mousePosition);

            if (e.keyCode == KeyCode.L && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
                ProcessContextLogicMenu(e.mousePosition);

            if (e.keyCode == KeyCode.C && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
                ProcessContextConstantMenu(e.mousePosition);

            if (e.keyCode == KeyCode.V && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
                ProcessContextCastingMenu(e.mousePosition);

            if (e.keyCode == KeyCode.S && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
                ProcessContextSelfMenu(e.mousePosition);

            if (e.keyCode == KeyCode.B && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
                boxSelectionMode = true;

            if (e.keyCode == KeyCode.Escape)
            {
                Instance.Connection_CleanConnectionPointSelection();
                preSelectionOutPoint = new ConnectionPoint[0];
                preSelectionInPoint = new ConnectionPoint[0];
                preSelectionChangeMode = false;
            }
                

            if (e.keyCode == KeyCode.C && e.shift && e.type == EventType.KeyDown)
                CenterViewer();

            if (e.keyCode == KeyCode.F && e.type == EventType.KeyDown)
                CenterSelectionNodes();

            if (e.keyCode == KeyCode.F1 && !e.control && !e.alt && !e.shift && e.type == EventType.KeyDown)
            {
                GreyBackground = GetHelpMessagePopup();
                GUI.changed = true;
            }

            if (e.keyCode == KeyCode.C && e.control && e.type == EventType.KeyDown)
            {
                OnClickCopy();
                GUI.changed = true;
            }

            if (e.keyCode == KeyCode.V && e.control && e.type == EventType.KeyDown)
            {
                OnClickPasteNodes(e.mousePosition);
                GUI.changed = true;
            }
                
            if ((e.keyCode == KeyCode.Delete || e.keyCode == KeyCode.X) && e.type == EventType.KeyDown)
            {
                DeleteSelection();
                GUI.changed = true;
            }

            if (e.keyCode == KeyCode.LeftBracket && e.type == EventType.KeyDown)
            {
                zoomLevel -= zoomGap;
                GUI.changed = true;
            }

            if (e.keyCode == KeyCode.RightBracket && e.type == EventType.KeyDown)
            {
                zoomLevel += zoomGap;
                GUI.changed = true;
            }

            if(e.keyCode == KeyCode.A && !e.shift && !e.control && !e.alt && e.type == EventType.KeyDown)
            {
                foreach(var i in selectBlueprint.nodes)
                {
                    if(i.page == selectionPage)
                    {
                        i.isSelected = true;
                    }
                }
                foreach(var i in selectBlueprint.connections)
                {
                    if(i.page == selectionPage)
                    {
                        i.isSelected = true;
                    }
                }
                GUI.changed = true;
            }

            if (boxSelectionMode)
            {
                OnBoxSelectionMode(e);
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
            DrawPreConnection(e);

            if (searchStruct != null) return;
            bool ClickAnyNode = false;

            for (int i = 0; i < selectBlueprint.nodes.Count; i++)
            {
                if (selectBlueprint.nodes[i].MouseIn(e.mousePosition) &&
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

            if (MouseInMenuBar(e.mousePosition))
                ClickAnyNode = true;

            if (!ClickAnyNode && e.type == EventType.MouseDown && e.button == 0) 
            {
                preSelectionOutPoint = new ConnectionPoint[0];
                preSelectionInPoint = new ConnectionPoint[0];
                preSelectionChangeMode = false;
                Instance.Node_CleanNodeSelection();
                Instance.Connection_CleanConnectionSelection();
                Instance.Connection_CleanConnectionPointSelection();
                selectedInPoint = null;
                selectedOutPoint = null;
                GUI.FocusControl(null);
                GUI.changed = true;
            }
        }

        public bool MouseInMenuBar(Vector2 mousePosition)
        {
            return new Rect(0, 0, position.width, 26).Contains(mousePosition);
        }

        /// <summary>
        /// Right click event in the editor
        /// </summary>
        /// <param name="mousePosition">Right click position</param>
        private void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            /* Adding content into menu */
            {
                for (int i = 0; i < allTypes.Length; i++)
                {
                    if (allTypes[i].IsSubclassOf(typeof(Node)))
                    {
                        NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                        if (nodePath != null)
                        {
                            addNode = allTypes[i];
                            genericMenu.AddItem(new GUIContent(nodePath.Path), false, Instance.GUI_OnClickAddNode, new AddClickEvent(mousePosition, allTypes[i], selectionPage));
                        }
                    }
                }
            }

            /* Adding search button into menu */
            {
                genericMenu.AddItem(new GUIContent("Search Node"), false, OnClickSearchNode, mousePosition);
            }

            /* Adding return node */
            {
                if (selectionPage > EBlueprint.DefaultPageCount - 1)
                {
                    if (selectBlueprint.blueprintEvent.customEvent[selectionPage - EBlueprint.DefaultPageCount].returnType != FieldType.Event)
                    {
                        genericMenu.AddItem(new GUIContent("Add Return Node"), false, Instance.GUI_OnClickAddReturnNode, new AddClickEvent() { mousePosition = mousePosition , page = selectionPage});
                    }
                }
            }

            /* Adding custom event into menu */
            {
                foreach(var i in selectBlueprint.GetPublicEvent())
                {
                    genericMenu.AddItem(new GUIContent("Add Node/Custom Event/Self/" + i.Item2.name + "." + i.Item1.eventName),
                        false, Instance.GUI_OnClickAddCustomEvent, new AddCustomEvent(mousePosition, i.Item2, i.Item1, selectionPage, false));
                }
            }

            /* Adding inherit custom event into menu */
            {
                if (selectBlueprint.Inherit != null)
                {
                    foreach (var i in selectBlueprint.GetInheritEvent())
                    {
                        genericMenu.AddItem(new GUIContent("Add Node/Custom Event/Inherit/" + i.Item2.name + "." + i.Item1.eventName),
                            false, Instance.GUI_OnClickAddCustomEvent, new AddCustomEvent(mousePosition, i.Item2, i.Item1, selectionPage, true));
                    }
                }
            }

            /* Adding edit button */
            {
                #region Copy & Delete
                if (Instance.Check_AnyConnectionSelect() || Instance.Check_AnyNodeSelect())
                {
                    genericMenu.AddItem(new GUIContent("Copy Selected"), false, OnClickCopy);
                    genericMenu.AddItem(new GUIContent("Delete Selected"), false, DeleteSelection);
                }
                #endregion

                #region Delete Select
                if (Instance.Check_AnyNodeSelect())
                {
                    genericMenu.AddItem(new GUIContent("Delete Selected Node"), false, Instance.Node_DeleteSelectionNode);
                }
                #endregion

                #region Delete Connection
                if (Instance.Check_AnyConnectionSelect())
                {
                    genericMenu.AddItem(new GUIContent("Delete Selected Connection"), false, Instance.Connection_DeleteSelectedConnection);
                }
                #endregion
            }

            /* Adding Paste command */
            {
                if (clipBorad != null)
                    genericMenu.AddItem(new GUIContent("Paste"), false, OnClickPasteNodes, mousePosition);
                else
                    genericMenu.AddDisabledItem(new GUIContent("Paste"));
            }

            /* Draw content on screen */
            genericMenu.ShowAsContext();
        }

        private void ProcessContextConstantMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            List<Tuple<int, List<Type>>> BufferNodes = new List<Tuple<int, List<Type>>>();

            /* Get list of type */
            {
                for (int i = 0; i < allTypes.Length; i++)
                {
                    if (allTypes[i].IsSubclassOf(typeof(Node)))
                    {
                        NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                        Constant_Menu constant_Menu = allTypes[i].GetCustomAttribute<Constant_Menu>();
                        if (nodePath != null && constant_Menu != null)
                        {
                            bool exist = false;
                            int index = 0;
                            foreach(var j in BufferNodes)
                            {
                                if (j.Item1 == constant_Menu.priority)
                                {
                                    exist = true;
                                    index = BufferNodes.IndexOf(j);
                                    break;
                                }
                            }

                            if (exist)
                            {
                                BufferNodes[index].Item2.Add(allTypes[i]);
                            }
                            else
                            {
                                BufferNodes.Add(new Tuple<int, List<Type>>(constant_Menu.priority, new List<Type>()));
                            }
                            //addNode = allTypes[i];
                        }
                    }
                }
            }

            /* Sort every list */
            {
                foreach (var i in BufferNodes)
                {
                    var q = from e in i.Item2 orderby e.GetCustomAttribute<NodePath>().Path.Split('/')[e.GetCustomAttribute<NodePath>().Path.Split('/').Length - 1] select e;
                    List<Type> buffer = new List<Type>();
                    foreach (var j in q)
                    {
                        buffer.Add(j);
                    }
                    i.Item2.Clear();
                    i.Item2.AddRange(buffer);
                }
            }

            /* Adding to context menu */
            {
                var q2 = from e in BufferNodes orderby e.Item1 select e;
                foreach (var i in q2)
                {
                    foreach (var j in i.Item2)
                    {
                        NodePath nodePath = j.GetCustomAttribute<NodePath>();
                        genericMenu.AddItem(new GUIContent(nodePath.Path.Split('/')[nodePath.Path.Split('/').Length - 1]), false, Instance.GUI_OnClickAddNode, new AddClickEvent(mousePosition, j, selectionPage));
                    }
                }
            }

            genericMenu.ShowAsContext();
        }

        private void ProcessContextSelfMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            /* Adding content into menu */
            {
                for (int i = 0; i < allTypes.Length; i++)
                {
                    if (allTypes[i].IsSubclassOf(typeof(Node)))
                    {
                        NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                        if (nodePath != null && allTypes[i].GetCustomAttribute<Self_Menu>() != null)
                        {
                            addNode = allTypes[i];
                            genericMenu.AddItem(new GUIContent(nodePath.Path.Split('/')[nodePath.Path.Split('/').Length - 1]), false, Instance.GUI_OnClickAddNode, new AddClickEvent(mousePosition, allTypes[i], selectionPage));
                        }
                    }
                }
            }

            genericMenu.ShowAsContext();
        }

        private void ProcessContextComponentMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            /* Adding content into menu */
            {
                for (int i = 0; i < allTypes.Length; i++)
                {
                    if (allTypes[i].IsSubclassOf(typeof(Node)))
                    {
                        NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                        if (nodePath != null && allTypes[i].GetCustomAttribute<Component_Menu>() != null)
                        {
                            addNode = allTypes[i];
                            genericMenu.AddItem(new GUIContent(nodePath.Path.Split('/')[nodePath.Path.Split('/').Length - 1]), false, Instance.GUI_OnClickAddNode, new AddClickEvent(mousePosition, allTypes[i], selectionPage));
                        }
                    }
                }
            }

            genericMenu.ShowAsContext();
        }

        private void ProcessContextCastingMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            /* Adding content into menu */
            {
                for (int i = 0; i < allTypes.Length; i++)
                {
                    if (allTypes[i].IsSubclassOf(typeof(Node)))
                    {
                        NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                        Casting_Menu casting_Menu = allTypes[i].GetCustomAttribute<Casting_Menu>();
                        if (nodePath != null && casting_Menu != null)
                        {
                            addNode = allTypes[i];
                            genericMenu.AddItem(new GUIContent(casting_Menu.source + "/" + nodePath.Path.Split('/')[nodePath.Path.Split('/').Length - 1]), false, Instance.GUI_OnClickAddNode, new AddClickEvent(mousePosition, allTypes[i], selectionPage));
                        }
                    }
                }
            }

            genericMenu.ShowAsContext();
        }

        private void ProcessContextMathMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            List<Tuple<string, Type>> allMathNodeType = new List<Tuple<string, Type>>();

            /* Getting all the math relate type */
            {
                for(int i = 0; i < allTypes.Length; i++)
                {
                    if (allTypes[i].IsSubclassOf(typeof(Node)))
                    {
                        NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                        Math_Menu math_Menu = allTypes[i].GetCustomAttribute<Math_Menu>();

                        if(nodePath != null && math_Menu != null)
                        {
                            allMathNodeType.Add(new Tuple<string, Type>(math_Menu.source + "/" + nodePath.Path.Split('/')[nodePath.Path.Split('/').Length - 1], allTypes[i]));
                        }
                    }
                }
            }

            /* Adding to menu */
            {
                var q = from e in allMathNodeType orderby e.Item1 select e;
                foreach(var i in q)
                {
                    genericMenu.AddItem(new GUIContent(i.Item1), false, Instance.GUI_OnClickAddNode, new AddClickEvent(mousePosition, i.Item2, selectionPage));
                }
            }

            genericMenu.ShowAsContext();
        }

        private void ProcessContextInputMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            List<Tuple<string, Type>> allMathNodeType = new List<Tuple<string, Type>>();

            /* Getting all the math relate type */
            {
                for (int i = 0; i < allTypes.Length; i++)
                {
                    if (allTypes[i].IsSubclassOf(typeof(Node)))
                    {
                        NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                        Input_Menu math_Menu = allTypes[i].GetCustomAttribute<Input_Menu>();

                        if (nodePath != null && math_Menu != null)
                        {
                            allMathNodeType.Add(new Tuple<string, Type>(math_Menu.source + "/" + nodePath.Path.Split('/')[nodePath.Path.Split('/').Length - 1], allTypes[i]));
                        }
                    }
                }
            }

            /* Adding to menu */
            {
                var q = from e in allMathNodeType orderby e.Item1 select e;
                foreach (var i in q)
                {
                    genericMenu.AddItem(new GUIContent(i.Item1), false, Instance.GUI_OnClickAddNode, new AddClickEvent(mousePosition, i.Item2, selectionPage));
                }
            }

            genericMenu.ShowAsContext();
        }

        private void ProcessContextEToolMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            List<Tuple<string, Type>> allMathNodeType = new List<Tuple<string, Type>>();

            /* Getting all the math relate type */
            {
                for (int i = 0; i < allTypes.Length; i++)
                {
                    if (allTypes[i].IsSubclassOf(typeof(Node)))
                    {
                        NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                        ETool_Menu math_Menu = allTypes[i].GetCustomAttribute<ETool_Menu>();

                        if (nodePath != null && math_Menu != null)
                        {
                            allMathNodeType.Add(new Tuple<string, Type>(math_Menu.source + "/" + nodePath.Path.Split('/')[nodePath.Path.Split('/').Length - 1], allTypes[i]));
                        }
                    }
                }
            }

            /* Adding to menu */
            {
                var q = from e in allMathNodeType orderby e.Item1 select e;
                foreach (var i in q)
                {
                    genericMenu.AddItem(new GUIContent(i.Item1), false, Instance.GUI_OnClickAddNode, new AddClickEvent(mousePosition, i.Item2, selectionPage));
                }
            }

            /* Adding custom event into menu */
            {
                foreach (var i in selectBlueprint.GetPublicEvent())
                {
                    genericMenu.AddItem(new GUIContent("Custom Event/Self/" + i.Item2.name + "." + i.Item1.eventName),
                        false, Instance.GUI_OnClickAddCustomEvent, new AddCustomEvent(mousePosition, i.Item2, i.Item1, selectionPage, false));
                }
            }

            /* Adding inherit custom event into menu */
            {
                if (selectBlueprint.Inherit != null)
                {
                    foreach (var i in selectBlueprint.GetInheritEvent())
                    {
                        genericMenu.AddItem(new GUIContent("Custom Event/Inherit/" + i.Item2.name + "." + i.Item1.eventName),
                            false, Instance.GUI_OnClickAddCustomEvent, new AddCustomEvent(mousePosition, i.Item2, i.Item1, selectionPage, true));
                    }
                }
            }

            genericMenu.ShowAsContext();
        }

        private void ProcessContextLogicMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            List<Tuple<string, Type>> allMathNodeType = new List<Tuple<string, Type>>();

            /* Getting all the math relate type */
            {
                for (int i = 0; i < allTypes.Length; i++)
                {
                    if (allTypes[i].IsSubclassOf(typeof(Node)))
                    {
                        NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                        Logic_Menu math_Menu = allTypes[i].GetCustomAttribute<Logic_Menu>();

                        if (nodePath != null && math_Menu != null)
                        {
                            allMathNodeType.Add(new Tuple<string, Type>(math_Menu.source + "/" + nodePath.Path.Split('/')[nodePath.Path.Split('/').Length - 1], allTypes[i]));
                        }
                    }
                }
            }

            /* Adding to menu */
            {
                var q = from e in allMathNodeType orderby e.Item1 select e;
                foreach (var i in q)
                {
                    genericMenu.AddItem(new GUIContent(i.Item1), false, Instance.GUI_OnClickAddNode, new AddClickEvent(mousePosition, i.Item2, selectionPage));
                }
            }

            genericMenu.ShowAsContext();
        }

        private void ProcessContextTransformMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            List<Tuple<string, Type>> allMathNodeType = new List<Tuple<string, Type>>();

            /* Getting all the math relate type */
            {
                for (int i = 0; i < allTypes.Length; i++)
                {
                    if (allTypes[i].IsSubclassOf(typeof(Node)))
                    {
                        NodePath nodePath = allTypes[i].GetCustomAttribute<NodePath>();
                        Transform_Menu math_Menu = allTypes[i].GetCustomAttribute<Transform_Menu>();

                        if (nodePath != null && math_Menu != null)
                        {
                            allMathNodeType.Add(new Tuple<string, Type>(math_Menu.source + "/" + nodePath.Path.Split('/')[nodePath.Path.Split('/').Length - 1], allTypes[i]));
                        }
                    }
                }
            }

            /* Adding to menu */
            {
                var q = from e in allMathNodeType orderby e.Item1 select e;
                foreach (var i in q)
                {
                    genericMenu.AddItem(new GUIContent(i.Item1), false, Instance.GUI_OnClickAddNode, new AddClickEvent(mousePosition, i.Item2, selectionPage));
                }
            }

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

        #endregion

        #region Utility
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
        /// <summary>
        /// Copy selection node <br />
        /// And delete the node that contain have [CanNotCopy]
        /// </summary>
        public void OnClickCopy()
        {
            List<NodeBase> nb = new List<NodeBase>();
            List<Connection> c = new List<Connection>();
            List<NodeBase> buffer = new List<NodeBase>();

            /* Search current nodes pool */
            for (int i = 0; i < selectBlueprint.nodes.Count; i++)
            {
                /* Node must be selected and cannot contain [CanNotCopy] attribute */
                if (selectBlueprint.nodes[i].isSelected && selectBlueprint.nodes[i].GetType().GetCustomAttribute<CanNotCopy>() == null)
                {
                    nb.Add(EBlueprint.MakeInstanceNode(selectBlueprint.nodes[i], selectBlueprint.blueprintVariables, selectBlueprint.blueprintEvent.customEvent, selectBlueprint.Inherit));
                    buffer.Add(selectBlueprint.nodes[i]);
                }
            }

            /* Search current connections pool */
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
            Vector2 pasteClickEvent = (Vector2)pasteEvent;
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
            Vector2 Diff = pasteClickEvent - OgcenterP;

            /* Spawn nodes */
            for (int i = 0; i < clipBorad.nodeBases.Count; i++)
            {
                AddClickEvent a = new AddClickEvent() { add = Type.GetType(clipBorad.nodeBases[i].NodeType), mousePosition = clipBorad.nodeBases[i].rect.position + Diff };
                NodeBase n = Instance.Node_AddNode(a.mousePosition, selectionPage, a.add);
                n.GivenValue(clipBorad.nodeBases[i]);
                for (int j = 0; j < n.fields.Count; j++)
                {
                    n.fields[j] = new Field(clipBorad.nodeBases[i].fields[j]);
                }
                n.DynamicFieldInitialize(null);
                buffer.Add(n);
                n.isSelected = true;
            }

            /* Spawn connection */
            for (int i = 0; i < clipBorad.connections.Count; i++)
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

                /* Update field state */
                buffer[clipBorad.connections[i].inPointMark.x].fields[clipBorad.connections[i].inPointMark.y].onConnection = true;
                buffer[clipBorad.connections[i].outPointMark.x].fields[clipBorad.connections[i].outPointMark.y].onConnection = true;
            }
        }

        public void DeleteSelection()
        {
            Instance.Connection_DeleteSelectedConnection();
            Instance.Node_DeleteSelectionNode();
        }

        public void OnClickInPoint(ConnectionPoint cp)
        {
            if (selectedInPoint != null)
                selectedInPoint.Selected = false;

            selectedInPoint = cp;
            cp.Selected = true;

            GUI.changed = true;
            Repaint();

            if (preSelectionChangeMode)
            {
                if (preSelectionChangeMode_Input)
                {
                    List<ConnectionPoint> buffer = new List<ConnectionPoint>(preSelectionOutPoint);

                    bool Pass = true;

                    for (int i = 0; i < buffer.Count; i++)
                    {
                        Connection c = selectBlueprint.Connection_GetConnectionByPoints(preSelectionInPoint[0], buffer[i]);
                        selectBlueprint.Connection_RemoveConnection(c);
                    }

                    for (int i = 0; i < buffer.Count; i++)
                    {
                        if (selectBlueprint.Connection_CheckConnectionWork(selectedInPoint, buffer[i]) == false) Pass = false;
                    }

                    if (Pass)
                    {
                        for (int i = 0; i < buffer.Count; i++)
                        {
                            popMessage = selectBlueprint.Connection_CreateConnection(selectedInPoint, buffer[i], selectionPage);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < buffer.Count; i++)
                        {
                            popMessage = selectBlueprint.Connection_CreateConnection(preSelectionInPoint[0], buffer[i], selectionPage);
                        }
                    }
                }
            }
            else
            {
                popMessage = selectBlueprint.Connection_CreateConnection(selectedInPoint, selectedOutPoint, selectionPage);
            }

            preSelectionOutPoint = new ConnectionPoint[0];
            preSelectionInPoint = new ConnectionPoint[0];
            preSelectionChangeMode = false;
        }

        public void OnClickOutPoint(ConnectionPoint cp)
        {
            if (selectedOutPoint != null)
                selectedOutPoint.Selected = false;

            selectedOutPoint = cp;
            cp.Selected = true;

            GUI.changed = true;
            Repaint();

            if (preSelectionChangeMode)
            {
                if (!preSelectionChangeMode_Input)
                {
                    List<ConnectionPoint> buffer = new List<ConnectionPoint>(preSelectionInPoint);

                    bool Pass = true;

                    for (int i = 0; i < buffer.Count; i++)
                    {
                        Connection c = selectBlueprint.Connection_GetConnectionByPoints(buffer[i], preSelectionOutPoint[0]);
                        selectBlueprint.Connection_RemoveConnection(c);
                    }

                    for (int i = 0; i < buffer.Count; i++)
                    {
                        if (selectBlueprint.Connection_CheckConnectionWork(buffer[i], selectedOutPoint) == false) Pass = false;
                    }

                    if (Pass)
                    {
                        for (int i = 0; i < buffer.Count; i++)
                        {
                            popMessage = selectBlueprint.Connection_CreateConnection(buffer[i], selectedOutPoint, selectionPage);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < buffer.Count; i++)
                        {
                            popMessage = selectBlueprint.Connection_CreateConnection(buffer[i], preSelectionOutPoint[0], selectionPage);
                        }
                    }

                    preSelectionOutPoint = new ConnectionPoint[0];
                    preSelectionInPoint = new ConnectionPoint[0];
                    preSelectionChangeMode = false;

                    if (selectedOutPoint != null)
                        selectedOutPoint.Selected = false;
                    selectedOutPoint = null;
                }
            }
        }

        public void OnUseTypeList(Field target)
        {
            typeListStruct = new TypeListStruct() { target = target };
        }

        public void OnChangeConnectionInputPoint(object _cp)
        {
            ConnectionPoint cp = (ConnectionPoint)_cp;
            Vector2Int v = selectBlueprint.Connection_GetConnectionInfo(cp);

            List<ConnectionPoint> connectionbuffer = new List<ConnectionPoint>();
            foreach(var i in selectBlueprint.connections)
            {
                if (i.inPointMark == v)
                {
                    connectionbuffer.Add(selectBlueprint.nodes[i.outPointMark.x].fields[i.outPointMark.y].outPoint);
                }
            }

            if (connectionbuffer.Count == 0) return;
            preSelectionInPoint = new ConnectionPoint[1] { cp };
            selectedInPoint = null;
            preSelectionChangeMode_Input = true;
            preSelectionChangeMode = true;
            preSelectionOutPoint = connectionbuffer.ToArray();
        }

        public void OnChangeConnectionOutputPoint(object _cp)
        {
            ConnectionPoint cp = (ConnectionPoint)_cp;
            Vector2Int v = selectBlueprint.Connection_GetConnectionInfo(cp);

            List<ConnectionPoint> connectionbuffer = new List<ConnectionPoint>();
            foreach (var i in selectBlueprint.connections)
            {
                if (i.outPointMark == v)
                {
                    connectionbuffer.Add(selectBlueprint.nodes[i.inPointMark.x].fields[i.inPointMark.y].outPoint);
                }
            }

            if (connectionbuffer.Count == 0) return;
            preSelectionOutPoint = new ConnectionPoint[1] { cp };
            selectedInPoint = null;
            preSelectionChangeMode_Input = false;
            preSelectionChangeMode = true;
            preSelectionInPoint = connectionbuffer.ToArray();
        }

        private void OnBoxSelectionMode(Event e)
        {
            if(e.type == EventType.MouseDown && e.button == 0)
            {
                boxIsSelecting = true;
                boxIsDeleteSelection = false;
                boxSelectionV1Point = e.mousePosition;
            }
            if (e.type == EventType.MouseDown && e.button == 2)
            {
                boxIsSelecting = true;
                boxIsDeleteSelection = true;
                boxSelectionV1Point = e.mousePosition;
            }

            if (e.type == EventType.MouseUp && e.button == 0)
            {
                boxSelectionMode = false;
                boxIsSelecting = false;
                BoxSelectExecute(GetBoxBy2Point(boxSelectionV1Point, boxSelectionV2Point), false);
            }
            if (e.type == EventType.MouseUp && e.button == 2)
            {
                boxSelectionMode = false;
                boxIsSelecting = false;
                BoxSelectExecute(GetBoxBy2Point(boxSelectionV1Point, boxSelectionV2Point), true);
            }

            if (boxIsSelecting)
            {
                boxSelectionV2Point = e.mousePosition;

                if(boxIsDeleteSelection)
                    GUI.Box(GetBoxBy2Point(boxSelectionV1Point, boxSelectionV2Point), "", StyleUtility.GetStyle(StyleType.BoxDeleteSelect));
                else
                    GUI.Box(GetBoxBy2Point(boxSelectionV1Point, boxSelectionV2Point), "", StyleUtility.GetStyle(StyleType.BoxSelect));

                GUI.changed = true;
            }
        }

        private void BoxSelectExecute(Rect range, bool delete)
        {
            foreach(var i in selectBlueprint.nodes)
            {
                bool fourPointIn = true;

                Rect target = i.GetBoxRect();

                Vector2[] fourPoint = new Vector2[4] 
                {
                    new Vector2(target.x, target.y),
                    new Vector2(target.x, target.y + target.height),
                    new Vector2(target.x + target.width, target.y),
                    new Vector2(target.x + target.width, target.y + target.height) 
                };

                for(int j = 0; j < 4; j++)
                {
                    if (!range.Contains(fourPoint[j])) fourPointIn = false;
                }

                if (fourPointIn)
                {
                    i.isSelected = !delete;
                }
            }

            foreach(var i in selectBlueprint.connections)
            {
                bool fourPointIn = true;

                Rect target = i.GetBoxRect();

                Vector2[] fourPoint = new Vector2[4]
                {
                    new Vector2(target.x, target.y),
                    new Vector2(target.x, target.y + target.height),
                    new Vector2(target.x + target.width, target.y),
                    new Vector2(target.x + target.width, target.y + target.height)
                };

                for (int j = 0; j < 4; j++)
                {
                    if (!range.Contains(fourPoint[j])) fourPointIn = false;
                }

                if (fourPointIn)
                {
                    i.isSelected = !delete;
                }
            }
        }

        public static Rect GetBoxBy2Point(Vector2 v1, Vector2 v2)
        {
            Rect buffer = new Rect();

            // v1 is at right down
            // v2 is at left up
            if(v1.x > v2.x && v1.y > v2.y)
            {
                buffer = new Rect(v2.x, v2.y, v1.x - v2.x, v1.y - v2.y);
            }

            // v1 is at right up
            // v2 is at left down
            else if (v1.x > v2.x && v1.y < v2.y)
            {
                buffer = new Rect(v2.x, v1.y, v1.x - v2.x, v2.y - v1.y);
            }

            // v1 is at left down
            // v2 is at right up
            else if (v1.x < v2.x && v1.y > v2.y)
            {
                buffer = new Rect(v1.x, v2.y, v2.x - v1.x, v1.y - v2.y);
            }

            // v1 is at left up
            // v2 is at right down
            else if (v1.x < v2.x && v1.y < v2.y)
            {
                buffer = new Rect(v1.x, v1.y, v2.x - v1.x, v2.y - v1.y);
            }

            return buffer;
        }
        #endregion

        #endregion


        ///
        /// State
        ///
        #region Editor State

        ///<summary>
        /// The method will prevent the list have repeat node instance
        ///</summary>
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
            if (allTypes == null && selectBlueprint != null)
            {
                allTypes = Instance.Node_GetAllNodebaseType();
            }
            if (selectBlueprint == null)
            {
                GreyBackground = new NodeEditorMessagePopup() { Message = "Please select blueprint", Okbutton = false };

                EBlueprint buffer = Selection.activeObject as EBlueprint;
                if (buffer != null)
                    selectBlueprint = buffer;
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
            if (popMessage != "" && popMessage != null)
            {
                DrawPopMessage();
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
                    DrawGreyBackgroundCenterText(GreyBackground.Message);
            }
            if(searchStruct != null)
            {
                DrawGreyBackground();
                DrawSearchMenu();
            }
            if(typeListStruct != null)
            {
                DrawGreyBackground();
                DrawTypeList();
            }
        }

        public ZoomData GetZoomLevel()
        {
            return new ZoomData()
            {
                maximum = 2.0f,
                minimum = 0.4f,
                ratio = zoomLevel,
                fieldHiddenLimit = 0.7f,
                titleHiddenLimit = 0.5f
            };
        }
        
        #endregion
    }
}
#endif
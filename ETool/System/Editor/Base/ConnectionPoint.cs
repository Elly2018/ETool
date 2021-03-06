﻿using UnityEditor;
using UnityEngine;

namespace ETool
{
    public enum ConnectionPointType { In, Out }

    /// <summary>
    /// The connection point part of data in field <br />
    /// Define the action <br />
    /// Interect with user
    /// </summary>
    [System.Serializable]
    public class ConnectionPoint
    {
        /// <summary>
        /// Drawing rect usually a squara <br />
        /// This is the width and height thickness of the rect
        /// </summary>
        public const float Thickness = 12f;

        /// <summary>
        /// The drawing rect
        /// </summary>
        public Rect rect;

        /// <summary>
        /// Define what field type this connection point is
        /// </summary>
        public FieldType fieldType = FieldType.Event;

        /// <summary>
        /// Define is in or out
        /// </summary>
        public ConnectionPointType type;

        /// <summary>
        /// Define drawing gui style
        /// </summary>
        public GUIStyle style;

        /// <summary>
        /// Define if it's selected
        /// </summary>
        public bool Selected = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">In Or Out</param>
        /// <param name="style">The GUI Style</param>
        public ConnectionPoint(ConnectionPointType type, GUIStyle style)
        {
            this.type = type;
            this.style = style;
            rect = new Rect(0, 0, 20f, 20f);
        }

        public ConnectionPoint(ConnectionPoint connectionPoint)
        {
            this.type = connectionPoint.type;
            this.style = connectionPoint.style;
            this.fieldType = connectionPoint.fieldType;
            rect = new Rect(0, 0, 20f, 20f);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Drawing method
        /// </summary>
        public void Draw()
        {
            /* Depend on input or output */
            /* If input => put the rect to left of field */
            /* If output => put the rect to right of field */
            switch (type)
            {
                case ConnectionPointType.In:
                    rect = new Rect(rect.x - Thickness, rect.y + Thickness, Thickness, Thickness);
                    break;

                case ConnectionPointType.Out:
                    rect = new Rect(rect.x + rect.width, rect.y + Thickness, Thickness, Thickness);
                    break;
            }

            /* Create a local style buffer */
            GUIStyle useStyle = new GUIStyle();

            /* If connection point is selected */
            /* Style normal background cover a texture with a color tint */
            if (Selected)
            {
                useStyle = new GUIStyle(style);
                useStyle.normal.background = new Texture2D(1, 1);
                Color[] tColor = new Color[useStyle.normal.background.width * useStyle.normal.background.height];
                for (int i = 0; i < useStyle.normal.background.width * useStyle.normal.background.height; i++)
                {
                    /* Each field type have different color */
                    tColor[i] = Field.GetColorByFieldType(fieldType, 0.2f);
                }
                useStyle.normal.background.SetPixels(0, 0, useStyle.normal.background.width, useStyle.normal.background.height, tColor);
            }
            /* If connection point is not selected */
            /* Use original style */
            else
            {
                useStyle = new GUIStyle(style);
            }

            /* Create a connection point button, and apply the style */
            GUI.color = Field.GetColorByFieldType(fieldType, 1);
            if (GUI.Button(rect, "", useStyle))
            {
                Event e = Event.current;
                if(e.button == 0)
                {
                    if (type == ConnectionPointType.In)
                        NodeBasedEditor.Editor_Instance.OnClickInPoint(this);
                    else
                    {
                        NodeBasedEditor.Editor_Instance.OnClickOutPoint(this);
                        NodeBasedEditor.Editor_Instance.PreConnectionPoint(this);
                    }
                }
                else if (e.button == 1)
                {
                    CreateProcessingContextMenu(e);
                }
            }

            GUI.color = Color.white;
        }

        private void CreateProcessingContextMenu(Event e)
        {
            GenericMenu genericMenu = new GenericMenu();

            if(type == ConnectionPointType.In)
            {
                genericMenu.AddItem(new GUIContent("Change Input"), false, NodeBasedEditor.Editor_Instance.OnChangeConnectionInputPoint, this);
            }

            if (type == ConnectionPointType.Out)
            {
                genericMenu.AddItem(new GUIContent("Change Output"), false, NodeBasedEditor.Editor_Instance.OnChangeConnectionOutputPoint, this);
            }


            genericMenu.ShowAsContext();
        }
        public void ProcessEvents(Event e)
        {

        }
#endif
    }
}
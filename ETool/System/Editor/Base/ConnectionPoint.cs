using System;
using UnityEngine;

namespace ETool
{
    public enum ConnectionPointType { In, Out }

    [System.Serializable]
    public class ConnectionPoint
    {
        /* This follow field rect */
        public Rect rect;
        public FieldType fieldType = FieldType.Event;

        public ConnectionPointType type;

        public GUIStyle style;

        public bool Selected = false;

        public ConnectionPoint(ConnectionPointType type, GUIStyle style)
        {
            this.type = type;
            this.style = style;
            rect = new Rect(0, 0, 20f, 20f);
        }

        public void Draw()
        {
            float Thickness = 12f;
            float Height = 16f;
            switch (type)
            {
                case ConnectionPointType.In:
                    rect = new Rect(rect.x - Thickness, rect.y + Height, Thickness, Thickness);
                    break;

                case ConnectionPointType.Out:
                    rect = new Rect(rect.x + rect.width, rect.y + Height, Thickness, Thickness);
                    break;
            }

            GUIStyle useStyle = new GUIStyle();

            if (Selected)
            {
                useStyle = new GUIStyle(style);
                useStyle.normal.background = new Texture2D(1, 1);
                Color[] tColor = new Color[useStyle.normal.background.width * useStyle.normal.background.height];
                for (int i = 0; i < useStyle.normal.background.width * useStyle.normal.background.height; i++)
                {
                    tColor[i] = Field.GetColorByFieldType(fieldType, 0.2f);
                }
                useStyle.normal.background.SetPixels(0, 0, useStyle.normal.background.width, useStyle.normal.background.height, tColor);
            }
            else
            {
                useStyle = new GUIStyle(style);
            }

            if (GUI.Button(rect, "", useStyle))
            {
                if (type == ConnectionPointType.In)
                    NodeBasedEditor.Instance.OnClickInPoint(this);
                else
                    NodeBasedEditor.Instance.OnClickOutPoint(this);
            }
        }

        public bool PointIn(Vector2 pos)
        {
            return rect.Contains(pos);
        }
    }
}
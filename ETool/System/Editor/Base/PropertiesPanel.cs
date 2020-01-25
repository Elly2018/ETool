using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    public enum PropertiesPanelType { Node, Edit, Setting, Help }

    public class PropertiesPanel
    {
        public Rect rect;
        public GUIStyle style;

        public Vector2 sizeLimit;
        public PropertiesPanelType propertiesPanelType;

        public PropertiesPanel(GUIStyle style, PropertiesPanelType panelType, Vector2 sizeLimit)
        {
            this.style = style;
            propertiesPanelType = panelType;
            this.sizeLimit = sizeLimit;
        }

        public void Height(float current)
        {
            rect = new Rect(rect.x, 30, rect.width, current);
        }

        public void Draw()
        {
            if (rect.width < sizeLimit.x) rect.width = sizeLimit.x;
            if (rect.width > sizeLimit.y) rect.width = sizeLimit.y;
            GUI.Box(rect, "", style);
        }

        public bool ProcessEvents(Event e)
        {
            return false;
        }
    }
}

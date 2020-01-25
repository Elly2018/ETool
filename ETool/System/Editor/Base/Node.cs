using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace ETool
{
    [System.Serializable]
    public class Node
    {
        public const float PropertiesHeight = 44;

        public Rect rect;
        public string title;
        public string description;
        public int page;

        public bool isDragged;
        public bool isSelected;

        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;
        public string NodeType;

        public List<Field> fields = new List<Field>();

        public Node(Vector2 position, float width, float height)
        {
            rect = new Rect(position.x, position.y, width, height);
            NodeType = this.GetType().FullName;
            if (fields == null) fields = new List<Field>();
        }

        public void Initialize()
        {
            FieldInitialize();
        }

        public virtual void FieldInitialize() { }
        public virtual void PostFieldInitialize() { }
        public virtual void PostFieldInitialize(BlueprintInput data) { }
        public virtual void DynamicFieldInitialize(BlueprintInput data) { }

        public virtual void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public void DrawField(List<Field> fs)
        {
            GUI.Label(rect, title, StyleUtility.GetStyle(StyleType.GUI_Title));
            for(int i = 0; i < fs.Count; i++)
            {
                /* Update field */
                fs[i].rect = new Rect(rect.x + 15f, rect.y + rect.height + (PropertiesHeight * i), rect.width - 30f, PropertiesHeight);
                fs[i].inPoint.rect = fs[i].rect;
                fs[i].outPoint.rect = fs[i].rect;

                fs[i].Draw();
            }
        }

        public virtual void ProcessCalling(BlueprintInput data) {}
        public virtual void FieldUpdate() { }
        public virtual void SelectionChanged(bool change) { }
        public virtual void DragChanged(bool change) { }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            GUI.changed = true;
                            isSelected = true;
                            SelectionChanged(isSelected);
                            DragChanged(isDragged);
                        }
                        else
                        {
                            if(!e.shift)
                            {
                                isSelected = false;
                            }
                            GUI.changed = true;
                            SelectionChanged(isSelected);
                        }
                    }
                    if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }
                    break;

                case EventType.MouseUp:
                    isDragged = false;
                    DragChanged(isDragged);
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && isSelected)
                    {
                        Drag(e.delta);
                        //e.Use();
                        return true;
                    }
                    break;
                case EventType.MouseMove:

                    break;
            }
            return false;
        }

        public virtual void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
            genericMenu.AddItem(new GUIContent("Description"), false, OnClickDescription);
            genericMenu.AddItem(new GUIContent("Copy"), false, OnClickCopy);
            genericMenu.ShowAsContext();
        }

        protected void OnClickDescription()
        {
            NodeBasedEditor.Instance.GreyBackgroundOkButton(description);
        }

        protected void OnClickRemoveNode()
        {
            NodeBasedEditor.Instance.OnClickRemoveSelectionNode();
        }

        protected void OnClickCopy()
        {
            NodeBasedEditor.Instance.OnClickCopyNodes();
        }
    }
}
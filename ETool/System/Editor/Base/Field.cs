using ETool.ANode;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    public enum ConnectionType
    {
        None,
        DataInput,
        DataOutput,
        DataBoth, 
        EventInput, 
        EventOutput,
        EventBoth,
    }

    public enum FieldContainer
    {
        Object, Array
    }

    public enum FieldType
    {
        Event = 0,
        Button = 1,

        // Basic data type
        Int = 10,
        Long = 11,
        Float = 12,
        String = 13,
        Boolean = 14,
        Double = 15,

        // Unity basic data type
        GameObject = 50,
        Object = 51,
        Vector2 = 52,
        Vector3 = 53,
        Vector4 = 54,
        Rect = 55,
        Color = 56,
        Type = 57,
        Variable = 58,
        Key = 59,
        Texture = 60,
        Material = 61,
        Component = 62,
        Quaternion = 63,
        Transform = 64,

        // Component
        Rigidbody = 200,
        Rigidbody2D = 201,
        Collision = 202,
        Collision2D = 203,
        Collider = 204,
        Collider2D = 205,
        MeshFilter = 206,
        MeshRenderer = 207,
    }

    [System.Serializable]
    public class Field
    {
        public Rect rect;
        public string title;
        public GenericObject target;
        public ConnectionType connectionType;
        public FieldType fieldType;
        public INode _iNode;
        public FieldContainer fieldContainer;

        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;

        public bool onConnection = false;
        public bool hideField = false;

        /// <summary>
        /// This contructor use for default field <br />
        /// Support basic data type <br />
        /// </summary>
        /// <param name="fieldType">Field Data Type</param>
        /// <param name="title">Field Title</param>
        /// <param name="connectionType">Connection IO</param>
        /// <param name="iNode">Node Interface</param>
        public Field(FieldType fieldType, string title, ConnectionType connectionType, INode iNode, FieldContainer fieldContainer)
        {
            target = new GenericObject();
            this.fieldType = fieldType;
            this.title = title;
            this.connectionType = connectionType;
            _iNode = iNode;
            inPoint = new ConnectionPoint(ConnectionPointType.In, StyleUtility.GetStyle(_iNode.GetInPointStyle()));
            outPoint = new ConnectionPoint(ConnectionPointType.Out, StyleUtility.GetStyle(_iNode.GetOutPointStyle()));
            this.fieldContainer = fieldContainer;
        }

        /// <summary>
        /// This contructor use for default field <br />
        /// Support basic data type <br />
        /// </summary>
        /// <param name="fieldType">Field Data Type</param>
        /// <param name="title">Field Title</param>
        /// <param name="connectionType">Connection IO</param>
        /// <param name="hide">Hide Inputfield</param>
        /// <param name="iNode">Node Interface</param>
        public Field(FieldType fieldType, string title, ConnectionType connectionType, bool hide, INode iNode, FieldContainer fieldContainer)
        {
            target = new GenericObject();
            this.fieldType = fieldType;
            this.title = title;
            this.connectionType = connectionType;
            _iNode = iNode;
            inPoint = new ConnectionPoint(ConnectionPointType.In, StyleUtility.GetStyle(_iNode.GetInPointStyle()));
            outPoint = new ConnectionPoint(ConnectionPointType.Out, StyleUtility.GetStyle(_iNode.GetOutPointStyle()));
            hideField = hide;
            this.fieldContainer = fieldContainer;
        }

        /// <summary>
        /// The constructor use reference to create instance
        /// </summary>
        /// <param name="reference">Reference</param>
        public Field(Field reference)
        {
            rect = new Rect(reference.rect);
            title = new string(reference.title.ToCharArray());
            target = reference.target;
            connectionType = reference.connectionType;
            fieldType = reference.fieldType;
            _iNode = reference._iNode;
            fieldContainer = reference.fieldContainer;

            inPoint = reference.inPoint;
            outPoint = reference.outPoint;

            onConnection = reference.onConnection;
            hideField = reference.hideField;
        }

        public virtual void Draw()
        {
            Rect Top = new Rect(rect);
            Rect Bottom = new Rect(rect);

            Top.height -= Top.height / 2;
            Bottom.height -= Bottom.height / 2;
            Bottom.y += Bottom.height;

            EditorGUI.BeginChangeCheck();
            switch (fieldType)
            {
                case FieldType.Int:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Int = EditorGUI.IntField(Bottom, target.target_Int);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.Float:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Float = EditorGUI.FloatField(Bottom, target.target_Float);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.String:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_String = EditorGUI.TextField(Bottom, target.target_String);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.Boolean:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Boolean = EditorGUI.Toggle(Bottom, target.target_Boolean);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.Double:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Double = EditorGUI.DoubleField(Bottom, target.target_Double);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.GameObject:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_GameObject = (GameObject)EditorGUI.ObjectField(Bottom, target.target_GameObject, typeof(GameObject), true);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Transform:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Transform = (Transform)EditorGUI.ObjectField(Bottom, target.target_Transform, typeof(Transform), true);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Vector2:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Vector2 = EditorGUI.Vector2Field(Bottom, "", target.target_Vector2);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.Vector3:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Vector3 = EditorGUI.Vector3Field(Bottom, "", target.target_Vector3);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.Vector4:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Vector4 = EditorGUI.Vector4Field(Bottom, "", target.target_Vector4);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.Rect:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Rect = EditorGUI.RectField(Bottom, target.target_Rect);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.Color:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Color = EditorGUI.ColorField(Bottom, target.target_Color);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Type:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Type = EditorGUI.Popup(Bottom, target.target_Type, Enum.GetNames(typeof(FieldType)));
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.Event:
                    {
                        GUI.Label(rect, title, StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.Variable:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Int = EditorGUI.Popup(Bottom, target.target_Int, GetVariableStringArray());
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
                    
                case FieldType.Key:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Int = (int)(KeyCode)EditorGUI.EnumPopup(Bottom, (KeyCode)target.target_Int);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Quaternion:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                        {
                            Vector4 q = new Vector4(target.target_Quaternion.x, target.target_Quaternion.y, target.target_Quaternion.z, target.target_Quaternion.w);
                            Vector4 resultQ = EditorGUI.Vector4Field(Bottom, "", q);
                            target.target_Quaternion = new Quaternion(resultQ.x, resultQ.y, resultQ.z, resultQ.w);
                        }
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Component:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_Int = EditorGUI.Popup(Bottom, target.target_Int, GetComponentFieldType_NameArray());
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Rigidbody:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_UnityObject = (Rigidbody)EditorGUI.ObjectField(Bottom, target.target_UnityObject, typeof(Rigidbody), true);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Collider:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        if (PrintField())
                            target.target_UnityObject = (Collider)EditorGUI.ObjectField(Bottom, target.target_UnityObject, typeof(Collider), true);
                        else
                            EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Collision:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }

                case FieldType.Collision2D:
                    {
                        EditorGUI.LabelField(Top, GetTypePrefixTitle(), StyleUtility.GetStyle(StyleType.GUI_Properties));
                        EditorGUI.LabelField(Bottom, "...", StyleUtility.GetStyle(StyleType.GUI_Properties));
                        break;
                    }
            }
            bool changed = EditorGUI.EndChangeCheck();
            if (changed)
                NodeBasedEditor.Instance.GetNodeByField(this).FieldUpdate();
            DrawPoint();
        }

        private string GetTypePrefixTitle()
        {
            return "(" + fieldType.ToString() + ")" + title;
        }

        private string[] GetVariableStringArray()
        {
            List<BlueprintVariable> bv = NodeBasedEditor.Instance.GetAllCustomVariable();
            string[] result = new string[bv.Count];
            for(int i = 0; i < bv.Count; i++)
            {
                result[i] = bv[i].label;
            }
            return result;
        }

        private bool PrintField()
        {
            return !onConnection && !hideField;
        }

        protected void DrawPoint()
        {
            switch (connectionType)
            {
                case ConnectionType.DataInput:
                    inPoint.Draw();
                    break;
                case ConnectionType.DataOutput:
                    outPoint.Draw();
                    break;
                case ConnectionType.DataBoth:
                    inPoint.Draw();
                    outPoint.Draw();
                    break;
                case ConnectionType.EventInput:
                    inPoint.Draw();
                    break;
                case ConnectionType.EventOutput:
                    outPoint.Draw();
                    break;
                case ConnectionType.EventBoth:
                    inPoint.Draw();
                    outPoint.Draw();
                    break;
            }
        }

        public static Color GetColorByFieldType(FieldType fieldType, float alpha)
        {
            Color c = Color.white;
            switch (fieldType)
            {
                case FieldType.Event:
                    break;
                case FieldType.Int:
                    break;
                case FieldType.Float:
                    break;
                case FieldType.String:
                    return Color.cyan;
                case FieldType.Boolean:
                    break;
                case FieldType.Double:
                    break;
                case FieldType.GameObject:
                    break;
                case FieldType.Vector2:
                    break;
                case FieldType.Vector3:
                    break;
                case FieldType.Vector4:
                    break;
                case FieldType.Rect:
                    break;
                case FieldType.Color:
                    break;
                case FieldType.Type:
                    break;
                case FieldType.Variable:
                    break;
            }
            return new Color(c.r, c.g, c.b, alpha);
        }

        public static Type GetTypeByFieldType(FieldType tf)
        {
            switch (tf)
            {
                case FieldType.Event:
                    return typeof(Nullable);
                case FieldType.Int:
                    return typeof(Int32);
                case FieldType.Float:
                    return typeof(Single);
                case FieldType.String:
                    return typeof(String);
                case FieldType.Boolean:
                    return typeof(Boolean);
                case FieldType.Double:
                    return typeof(Double);
                case FieldType.GameObject:
                    return typeof(GameObject);
                case FieldType.Vector2:
                    return typeof(Vector2);
                case FieldType.Vector3:
                    return typeof(Vector3);
                case FieldType.Vector4:
                    return typeof(Vector4);
                case FieldType.Rect:
                    return typeof(Rect);
                case FieldType.Color:
                    return typeof(Color);
                case FieldType.Type:
                    return typeof(FieldType);
                case FieldType.Variable:
                    break;
            }
            return typeof(Nullable);
        }

        public static object GetObjectByFieldType(FieldType tf, GenericObject go)
        {
            switch (tf)
            {
                case FieldType.Int:
                    return go.target_Int;
                case FieldType.Float:
                    return go.target_Float;
                case FieldType.String:
                    return go.target_String;
                case FieldType.Boolean:
                    return go.target_Boolean;
                case FieldType.Double:
                    return go.target_Double;
                case FieldType.GameObject:
                    return go.target_GameObject;
                case FieldType.Vector2:
                    return go.target_Vector2;
                case FieldType.Vector3:
                    return go.target_Vector3;
                case FieldType.Vector4:
                    return go.target_Vector4;
                case FieldType.Rect:
                    return go.target_Rect;
                case FieldType.Color:
                    return go.target_Color;
                case FieldType.Type:
                    return go.target_Type;
                case FieldType.Variable:
                    break;
                case FieldType.Collision:
                    return go.target_UnityObject;
                case FieldType.Event:
                    break;
                case FieldType.Button:
                    break;
                case FieldType.Long:
                    break;
                case FieldType.Object:
                    break;
                case FieldType.Key:
                    break;
                case FieldType.Texture:
                    break;
                case FieldType.Material:
                    break;
                case FieldType.Component:
                    break;
                case FieldType.Quaternion:
                    break;
                case FieldType.Transform:
                    break;
                case FieldType.Rigidbody:
                    break;
                case FieldType.Rigidbody2D:
                    break;
                case FieldType.Collision2D:
                    break;
                case FieldType.Collider:
                    break;
                case FieldType.Collider2D:
                    break;
                case FieldType.MeshFilter:
                    break;
                case FieldType.MeshRenderer:
                    break;
            }
            return typeof(Nullable);
        }

        public static object SetObjectByFieldType(FieldType tf, GenericObject go, object o)
        {
            switch (tf)
            {
                case FieldType.Int:
                    go.target_Int = (int)o;
                    return go.target_Int;
                case FieldType.Float:
                    go.target_Float = (float)o;
                    return go.target_Float;
                case FieldType.String:
                    return go.target_String;
                case FieldType.Boolean:
                    return go.target_Boolean;
                case FieldType.Double:
                    return go.target_Double;
                case FieldType.GameObject:
                    return go.target_GameObject;
                case FieldType.Vector2:
                    return go.target_Vector2;
                case FieldType.Vector3:
                    go.target_Vector3 = (Vector3)o;
                    return go.target_Vector3;
                case FieldType.Vector4:
                    return go.target_Vector4;
                case FieldType.Rect:
                    return go.target_Rect;
                case FieldType.Color:
                    return go.target_Color;
                case FieldType.Type:
                    return go.target_Type;
                case FieldType.Variable:
                    break;
            }
            return typeof(Nullable);
        }

        public static string[] GetComponentFieldType_NameArray()
        {
            List<string> result = new List<string>();
            foreach(var i in Enum.GetNames(typeof(FieldType)))
            {
                if(((int)(FieldType)Enum.Parse(typeof(FieldType), i)) >= 200)
                {
                    result.Add(i);
                }
            }
            return result.ToArray();
        }

        public static int[] GetComponentFieldType_IndexArray()
        {
            List<int> result = new List<int>();
            foreach (var i in Enum.GetNames(typeof(FieldType)))
            {
                if (((int)(FieldType)Enum.Parse(typeof(FieldType), i)) >= 200)
                {
                    result.Add(((int)(FieldType)Enum.Parse(typeof(FieldType), i)));
                }
            }
            return result.ToArray();
        }
    }
}

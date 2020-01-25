using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ETool
{
    public class EToolEditorBase : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawTitle(GetEditorName());
            DrawEToolInformation();
            serializedObject.ApplyModifiedProperties();
            if (GUI.changed && !Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        public virtual void DrawEToolInformation()
        {
            base.OnInspectorGUI();
        }

        public virtual string GetEditorName()
        {
            return serializedObject.FindProperty("m_Name").stringValue;
        }

        public virtual GUIStyle GetEditorTitleSkin()
        {
            GUIStyle defaultSkin = new GUIStyle();
            defaultSkin.fontSize = 12;
            defaultSkin.alignment = TextAnchor.MiddleCenter;
            defaultSkin.fontStyle = FontStyle.Bold;
            return defaultSkin;
        }

        protected void VGroupStart()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUI.indentLevel++;
        }

        protected void VGroupEnd()
        {
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        protected void HGroupStart()
        {
            EditorGUILayout.BeginHorizontal("Box");
        }

        protected void HGroupEnd()
        {
            EditorGUILayout.EndHorizontal();
        }

        protected void DrawTitle2(string message)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 12;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleLeft;
            EditorGUILayout.LabelField(message, style);
        }

        protected GenericObject DrawFieldHelper(GenericObject o, string t)
        {
            // Int
            if(t == typeof(Int32).FullName)
                o.target_Int = EditorGUILayout.IntField(o.target_Int);
            // Single
            else if (t == typeof(Single).FullName)
                o.target_Float = EditorGUILayout.FloatField(o.target_Float);
            // String
            else if (t == typeof(String).FullName)
                o.target_String = EditorGUILayout.TextField(o.target_String);
            // Double
            else if (t == typeof(Double).FullName)
                o.target_Double = EditorGUILayout.DoubleField(o.target_Double);
            // Bool
            else if (t == typeof(Boolean).FullName)
                o.target_Boolean = EditorGUILayout.Toggle(o.target_Boolean);
            // Color
            else if (t == typeof(Color).FullName)
                o.target_Color = EditorGUILayout.ColorField(o.target_Color);
            // Vector2
            else if (t == typeof(Vector2).FullName)
                o.target_Vector2 = EditorGUILayout.Vector2Field("", o.target_Vector2);
            // Vector3
            else if (t == typeof(Vector3).FullName)
                o.target_Vector3 = EditorGUILayout.Vector3Field("", o.target_Vector3);
            // Vector4
            else if (t == typeof(Vector4).FullName)
                o.target_Vector4 = EditorGUILayout.Vector4Field("", o.target_Vector4);
            // Rect
            else if (t == typeof(Rect).FullName)
                o.target_Rect = EditorGUILayout.RectField(o.target_Rect);
            // Vector4
            else if (t == typeof(FieldType).FullName)
                o.target_Type = (int)((FieldType)EditorGUILayout.EnumPopup((FieldType)o.target_Type));
            // GameObject
            else if (t == typeof(GameObject).FullName)
                o.target_GameObject = (GameObject)EditorGUILayout.ObjectField((GameObject)o.target_GameObject, typeof(GameObject), true);

            return o;
        }

        /// <summary>
        /// Element's data type
        /// </summary>
        /// <param name="e"></param>
        protected string DrawElementDataType(string og)
        {
            Type[] All = GetAllType();
            List<Type> allList = new List<Type>(All);
            List<string> testd = new List<string>();
            for (int i = 0; i < All.Length; i++)
            {
                testd.Add(All[i].FullName);
            }

            int typeIndex = EditorGUILayout.Popup(testd.IndexOf(og), testd.ToArray());

            if (typeIndex < 0 || typeIndex > All.Length)
                typeIndex = 0;
            return All[typeIndex].FullName;
        }

        /// <summary>
        /// Element's data type
        /// </summary>
        /// <param name="og"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        protected string DrawElementDataType(string og, string label)
        {
            Type[] All = GetAllType();
            List<Type> allList = new List<Type>(All);
            List<string> testd = new List<string>();
            for(int i = 0; i < All.Length; i++)
            {
                testd.Add(All[i].FullName);
            }

            int typeIndex = EditorGUILayout.Popup(label, testd.IndexOf(og), testd.ToArray());

            if (typeIndex < 0 || typeIndex > All.Length) 
                typeIndex = 0;

            return All[typeIndex].FullName;
        }

        protected Type[] GetAllType()
        {
            List<Type> test = new List<Type>();
            test.Add(typeof(Int32));
            test.Add(typeof(Single));
            test.Add(typeof(String));
            test.Add(typeof(Boolean));
            test.Add(typeof(Color));
            test.Add(typeof(Vector2));
            test.Add(typeof(Vector3));
            test.Add(typeof(Vector4));
            test.Add(typeof(GameObject));
            test.AddRange(OtherType());
            return test.ToArray();
        }

        protected Type[] OtherType()
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            Type[] all = ass.GetTypes();
            List<Type> mystruct = new List<Type>();

            foreach (var i in all)
            {
                if (i.IsSubclassOf(typeof(ObjectBase))) mystruct.Add(i);
            }

            return mystruct.ToArray();
        }

        private void DrawTitle(string _label)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(); EditorGUILayout.Space();
            EditorGUILayout.LabelField(_label, GetEditorTitleSkin());
            EditorGUILayout.Space(); EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
    }
}

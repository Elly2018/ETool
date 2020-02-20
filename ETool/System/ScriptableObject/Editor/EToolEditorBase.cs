#if UNITY_EDITOR
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

        protected void DrawTitle3(string message)
        {
            GUIStyle style = new GUIStyle();
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 10;
            style.alignment = TextAnchor.MiddleLeft;
            EditorGUILayout.LabelField(message, style);
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
            string[] array = Enum.GetNames(typeof(FieldType));
            
            foreach(var i in array)
            {
                test.Add(Field.GetTypeByFieldType((FieldType)Enum.Parse(typeof(FieldType), i)));
            }

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
#endif
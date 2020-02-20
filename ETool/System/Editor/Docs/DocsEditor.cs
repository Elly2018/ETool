#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    [CustomEditor(typeof(EDocs))]
    [CanEditMultipleObjects]
    public class DocsEditor : EToolEditorBase
    {
        private Assembly ass;
        private Type[] allType;
        private DocsBase page;

        public static DocsEditor Instance 
        {
            get
            {
                return (DocsEditor)Resources.FindObjectsOfTypeAll(typeof(DocsEditor)).FirstOrDefault();
            }
        }

        private void OnEnable()
        {
            allType = ReadAllType();
            ChangePage(serializedObject.FindProperty("Path").stringValue);
        }

        public override void DrawEToolInformation()
        {
            VGroupStart();
            LanRender();
            PathRender();
            if (page != null)
            {
                page.Render();
                page.LanIndex = serializedObject.FindProperty("LanmIndex").intValue;
            }
                
            HGroupEnd();
        }

        private void LanRender()
        {
            HGroupStart();
            if (GUILayout.Button("EN"))
            {
                serializedObject.FindProperty("LanmIndex").intValue = 0;
            }
            if (GUILayout.Button("TW"))
            {
                serializedObject.FindProperty("LanmIndex").intValue = 1;
            }
            if (GUILayout.Button("CH"))
            {
                serializedObject.FindProperty("LanmIndex").intValue = 2;
            }
            HGroupEnd();
        }

        private Type[] ReadAllType()
        {
            List<Type> result = new List<Type>();
            ass = Assembly.GetExecutingAssembly();
            for (int i = 0; i < ass.GetTypes().Length; i++)
            {
                if (ass.GetTypes()[i].IsSubclassOf(typeof(DocsBase)) && ass.GetTypes()[i].GetCustomAttribute<DocsPath>() != null)
                {
                    result.Add(ass.GetTypes()[i]);
                }
            }
            return result.ToArray();
        }

        private void PathRender()
        {
            string[] p = serializedObject.FindProperty("Path").stringValue.Split('/');
            HGroupStart();

            for(int i = 0; i < p.Length; i++)
            {
                if (GUILayout.Button(p[i], GUILayout.Width(100)))
                {
                    string buffer = p[0];
                    for(int j = 1; j < i + 1; j++)
                    {
                        buffer += "/" + p[j];
                    }

                    serializedObject.FindProperty("Path").stringValue = buffer;
                    ChangePage(serializedObject.FindProperty("Path").stringValue);
                }
            }
            HGroupEnd();
        }

        public void ChangePage(string path)
        {
            serializedObject.FindProperty("Path").stringValue = path;
            foreach (var i in allType)
            {
                if(i.GetCustomAttribute<DocsPath>().Path == path)
                {
                    page = (DocsBase)ass.CreateInstance(i.FullName);
                    return;
                }
            }
            Debug.LogWarning("cannot find page: " + path);
        }
    }
}
#endif
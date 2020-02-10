using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace ETool
{
    /// <summary>
    /// The language struct is use for store language labels
    /// It will be a scriptable object form store in asset
    /// User can import json file and export json file to change language value
    /// </summary>
    [CreateAssetMenu(menuName = "ETool/Language Struct")]
    public class ELanguageStruct : ScriptableObject
    {
        /// <summary>
        /// The class that store language data package
        /// </summary>
        [System.Serializable]
        public class LanguagePackage
        {
            /// <summary>
            /// List of language elements
            /// It contain tag and label
            /// Tag use for search
            /// Label use for string data that show on screen
            /// </summary>
            public List<LanguageElement> languageElements;

            /// <summary>
            /// Constrctor, prevent list be null value
            /// </summary>
            public LanguagePackage()
            {
                languageElements = new List<LanguageElement>();
            }
        }

        /// <summary>
        /// Language package variable
        /// </summary>
        public LanguagePackage languagePack = new LanguagePackage();

        #region Editor Button Event
        /// <summary>
        /// Reset the language struct
        /// It will make list match the node count and all the string will be null
        /// </summary>
        public void Reset_Node_Func()
        {
            Type[] types = GetAllNodebaseType();
            languagePack = new LanguagePackage();
            for(int i = 0; i < types.Length; i++)
            {
                NodePath np = types[i].GetCustomAttribute<NodePath>();
                string title_Default = np.Path.Replace("Add Node", "Node");
                title_Default = title_Default.Replace("/", ".");
                languagePack.languageElements.Add(new LanguageElement() { tag = title_Default + ".Title", target = "" });
                languagePack.languageElements.Add(new LanguageElement() { tag = title_Default + ".Des", target = "" });
            }
        }

        /// <summary>
        /// Adding the new node string register object
        /// This will keep the original data, and import the language that miss
        /// </summary>
        public void Reset_Node_Func_Keep()
        {
            Type[] types = GetAllNodebaseType();
            for (int i = 0; i < types.Length; i++)
            {
                NodePath np = types[i].GetCustomAttribute<NodePath>();
                string title_Default = np.Path.Replace("Add Node", "Node");
                title_Default = title_Default.Replace("/", ".");
                string des_Default = title_Default + ".Des";
                title_Default = title_Default + ".Title";

                bool TExist = false;
                bool DExist = false;
                for(int j = 0; j < languagePack.languageElements.Count; j++)
                {
                    if (title_Default == languagePack.languageElements[j].tag)
                    {
                        TExist = true;
                    }
                    if (des_Default == languagePack.languageElements[j].tag)
                    {
                        DExist = true;
                    }
                }

                if (!TExist)
                {
                    languagePack.languageElements.Add(new LanguageElement() { tag = title_Default, target = "" });
                }
                if (!DExist)
                {
                    languagePack.languageElements.Add(new LanguageElement() { tag = des_Default, target = "" });
                }
            }
        }

        /// <summary>
        /// Reset the field struct
        /// It will make list match the field type count
        /// </summary>
        public void Reset_Field_Func()
        {

        }

        public void Reset_Field_Func_Keep()
        {

        }

        public void Reset_Custom_Field()
        {
            languagePack = new LanguagePackage();
        }

        public void Import()
        {
            string filename = EditorUtility.OpenFilePanel("Import From Json File", "", "json");
            if(filename != null)
            {
                LanguagePackage buffer = JsonUtility.FromJson<LanguagePackage>(File.ReadAllText(filename));
                if(buffer != null)
                    languagePack.languageElements = buffer.languageElements;
            }
        }

        public void Export()
        {
            string filename = EditorUtility.SaveFilePanel("Export To Json File", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "", "json");
            if(filename != null)
            {
                string buffer = JsonUtility.ToJson(languagePack, true);
                File.WriteAllText(filename, buffer);
            }
        }

        #endregion
        private Type[] GetAllNodebaseType()
        {
            Assembly assmbly = Assembly.GetExecutingAssembly();
            Type[] allTypes = assmbly.GetTypes();
            List<ForNodeNameSort> search = new List<ForNodeNameSort>();
            foreach (var i in allTypes)
            {
                NodePath np = i.GetCustomAttribute<NodePath>();
                if (np != null)
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
    }

    [System.Serializable]
    public class LanguageElement
    {
        public string tag = "New Tag";
        [TextArea(0, 10)] public string target = "New Target";
    }
}

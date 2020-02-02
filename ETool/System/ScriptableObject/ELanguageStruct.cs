using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

namespace ETool
{
    [CreateAssetMenu(menuName = "ETool/Language Struct")]
    public class ELanguageStruct : ScriptableObject
    {
        public List<LanguageElement> languageElements = new List<LanguageElement>();

        public void Reset_Node_Func()
        {
            Type[] types = GetAllNodebaseType();
            languageElements.Clear();
            for(int i = 0; i < types.Length; i++)
            {
                NodePath np = types[i].GetCustomAttribute<NodePath>();
                string title_Default = np.Path.Replace("Add Node", "Node");
                title_Default = title_Default.Replace("/", ".");
                languageElements.Add(new LanguageElement() { tag = title_Default + ".Title", target = "" });
                languageElements.Add(new LanguageElement() { tag = title_Default + ".Des", target = "" });
            }
        }

        public void Reset_Field_Func()
        {

        }

        public void Reset_Custom_Field()
        {
            languageElements = new List<LanguageElement>();
        }

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

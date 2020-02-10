using System;
using System.Reflection;
using UnityEngine;

namespace ETool
{
    public class EToolString
    {
        public static string GetString_Node(string tag, string defaultString)
        {
            if (ToolLanguageEditor.languageManager.node_LanguageStructs.Count == 0) return defaultString;
            ELanguageStruct es = ToolLanguageEditor.languageManager.node_LanguageStructs[ToolLanguageEditor.languageManager.node_Index];

            if(es != null)
            {
                foreach (var i in es.languagePack.languageElements)
                {
                    if (i.tag == tag)
                    {
                        string result = i.target;
                        result = result.Replace("\\n", "\n");

                        if (result == "")
                            return defaultString;
                        else
                            return result;
                    }
                }
            }
            return defaultString;
        }

        public static string GetString_Field(string tag, string defaultString)
        {
            if (ToolLanguageEditor.languageManager.field_LanguageStructs.Count == 0) return defaultString;
            ELanguageStruct es = ToolLanguageEditor.languageManager.field_LanguageStructs[ToolLanguageEditor.languageManager.field_Index];

            if(es != null)
            {
                foreach (var i in es.languagePack.languageElements)
                {
                    if (i.tag == tag)
                    {
                        string result = i.target;
                        result = result.Replace("\\n", "\n");

                        if (result == "")
                            return defaultString;
                        else
                            return result;
                    }
                }
            }
            return defaultString;
        }

        public static string GetString_Custom(string tag, string defaultString)
        {
            if (ToolLanguageEditor.languageManager.custom_LanguageStructs.Count == 0) return defaultString;
            ELanguageStruct es = ToolLanguageEditor.languageManager.custom_LanguageStructs[ToolLanguageEditor.languageManager.custom_Index];

            if (es != null)
            {
                foreach (var i in es.languagePack.languageElements)
                {
                    if (i.tag == tag)
                    {
                        string result = i.target;
                        result = result.Replace("\\n", "\n");

                        if (result == "")
                            return defaultString;
                        else
                            return result;
                    }
                }
            }
            return defaultString;
        }

        public static string GetNodeTitle(Type t)
        {
            NodePath np = t.GetCustomAttribute<NodePath>();
            if (np!= null)
            {
                string result = np.Path;
                result = result.Replace("/", ".");
                result = result.Replace("Add Node", "Node");
                result += ".Title";
                return result;
            }
            return "";
        }

        public static string GetNodeDes(Type t)
        {
            NodePath np = t.GetCustomAttribute<NodePath>();
            if (np != null)
            {
                string result = np.Path;
                result = result.Replace("/", ".");
                result = result.Replace("Add Node", "Node");
                result += ".Des";
                return result;
            }
            return "";
        }
    }
}

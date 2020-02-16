using System.IO;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    public class ToolLanguageEditor : EditorWindow
    {
        public static ELanguageManager languageManager = new ELanguageManager();
        [SerializeField] private ELanguageManager _languageManager;
        private Editor editor;

        private static ToolLanguageEditor NBE;
        public static ToolLanguageEditor Instance
        {
            get
            {
                //Debug.LogWarning("Call Instance");
                if (NBE == null)
                    NBE = GetWindow<ToolLanguageEditor>();
                return NBE;
            }
        }

        [MenuItem("ETool/Language Register")]
        public static void OpenWindow()
        {
            /* \\\uwu\\\ seens somebody is calling me hehe */
            ToolLanguageEditor.NBE = GetWindow<ToolLanguageEditor>();
            ToolLanguageEditor.NBE.titleContent = new GUIContent("Language Register");
        }

        private void OnEnable()
        {
            Init();
        }

        private void OnDisable()
        {
            Init();
        }

        private void OnFocus()
        {
            Init();
        }

        private void OnValidate()
        {
            languageManager = _languageManager;
            Save();
        }

        private void Init()
        {
            Load();
            languageManager = _languageManager;
        }

        private void OnGUI()
        {
            if(!editor)
                editor = Editor.CreateEditor(this);

            if (editor)
            {
                if(GUILayout.Button("Open temp folder"))
                {
                    Application.OpenURL(Application.temporaryCachePath);
                }
                EditorGUILayout.Space();

                string[] lan_tag = GetLanTag(_languageManager, 0); // Node
                _languageManager.node_Index = EditorGUILayout.Popup("Node Select", _languageManager.node_Index, lan_tag);
                EditorGUILayout.PropertyField(editor.serializedObject.FindProperty("_languageManager").FindPropertyRelative("node_LanguageStructs"), true);
                EditorGUILayout.Space();

                lan_tag = GetLanTag(_languageManager, 1); // Field
                _languageManager.field_Index = EditorGUILayout.Popup("Field Select", _languageManager.field_Index, lan_tag);
                EditorGUILayout.PropertyField(editor.serializedObject.FindProperty("_languageManager").FindPropertyRelative("field_LanguageStructs"), true);
                EditorGUILayout.Space();

                lan_tag = GetLanTag(_languageManager, 2); // Custom
                _languageManager.custom_Index = EditorGUILayout.Popup("Custom Select", _languageManager.custom_Index, lan_tag);
                EditorGUILayout.PropertyField(editor.serializedObject.FindProperty("_languageManager").FindPropertyRelative("custom_LanguageStructs"), true);
                EditorGUILayout.Space();

                editor.serializedObject.ApplyModifiedProperties();
            }
        }

        public void Load()
        {
            if (!File.Exists(Path.Combine(Application.temporaryCachePath, "ETool_Language.json")))
            {
                CreateDefault();
            }

            string o = File.ReadAllText(Path.Combine(Application.temporaryCachePath, "ETool_Language.json"));
            ELanguageManager t = JsonUtility.FromJson<ELanguageManager>(o);

            if(t != null)
            {
                _languageManager = t;
            }
        }

        public void Save()
        {
            string o = JsonUtility.ToJson(_languageManager, true);
            File.WriteAllText(Path.Combine(Application.temporaryCachePath, "ETool_Language.json"), o);
        }

        public static void CreateDefault()
        {
            ELanguageManager e = new ELanguageManager();
            e.node_LanguageStructs.Add(AssetDatabase.LoadAssetAtPath<ELanguageStruct>("Assets/ETool/Language/Node/Node_EN.asset"));
            e.node_LanguageStructs.Add(AssetDatabase.LoadAssetAtPath<ELanguageStruct>("Assets/ETool/Language/Node/Node_CH.asset"));
            string o = JsonUtility.ToJson(e, true);
            File.WriteAllText(Path.Combine(Application.temporaryCachePath, "ETool_Language.json"), o);
        }

        private string[] GetLanTag(ELanguageManager em, int index)
        {
            string[] e = new string[0];
            switch (index)
            {
                case 0:
                    e = new string[em.node_LanguageStructs.Count];
                    for(int i = 0; i < e.Length; i++)
                    {
                        if (em.node_LanguageStructs[i] != null)
                            e[i] = em.node_LanguageStructs[i].name;
                        else
                            e[i] = "";
                    }
                    break;
                case 1:
                    e = new string[em.field_LanguageStructs.Count];
                    for (int i = 0; i < e.Length; i++)
                    {
                        if (em.field_LanguageStructs[i] != null)
                            e[i] = em.field_LanguageStructs[i].name;
                        else
                            e[i] = "";
                    }
                    break;
                case 2:
                    e = new string[em.custom_LanguageStructs.Count];
                    for (int i = 0; i < e.Length; i++)
                    {
                        if (em.custom_LanguageStructs[i] != null)
                            e[i] = em.custom_LanguageStructs[i].name;
                        else
                            e[i] = "";
                    }
                    break;
            }
            return e;
        }
    }
}

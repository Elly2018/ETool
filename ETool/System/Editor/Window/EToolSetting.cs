using UnityEditor;
using UnityEngine;

namespace ETool
{
#if UNITY_EDITOR
    public class EToolSetting : EditorWindow
    {
        private static EToolSetting NBE;

        [MenuItem("ETool/Language Register")]
        public static void OpenWindow()
        {
            /* \\\uwu\\\ seens somebody is calling me hehe */
            EToolSetting.NBE = GetWindow<EToolSetting>();
            EToolSetting.NBE.titleContent = new GUIContent("ETool Setting");
        }

        private void OnFocus()
        {
            
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void OnGUI()
        {
            
        }
    }
#endif
}

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ETool
{
    public abstract class DocsBase
    {
        GUIStyle title;
        GUIStyle title2;
        GUIStyle title3;
        GUIStyle image;
        GUIStyle p;

        public int LanIndex;

        public Color DarkGrey
        {
            get
            {
                return new Color(0.2f, 0.2f, 0.2f, 1.0f);
            }
        }

        public DocsBase()
        {
            title = new GUIStyle();
            title2 = new GUIStyle();
            title3 = new GUIStyle();
            image = new GUIStyle();
            p = new GUIStyle();

            title.fontSize = 26;
            title2.fontSize = 18;
            title3.fontSize = 14;

            title.fontStyle = FontStyle.Bold;
            title2.fontStyle = FontStyle.Bold;
            title3.fontStyle = FontStyle.Bold;

            title.alignment = TextAnchor.MiddleCenter;
            title2.alignment = TextAnchor.MiddleCenter;
            title3.alignment = TextAnchor.MiddleCenter;
            image.alignment = TextAnchor.MiddleCenter;
            p.alignment = TextAnchor.MiddleCenter;
        }

        public abstract void Render();

        private int GetRealIndex(int length, int index)
        {
            if(index < 0 || index >= length)
            {
                return 0;
            }
            else
            {
                return index;
            }
        }

        #region Paragraph
        public void Paragraph(string text)
        {
            p.normal.textColor = DarkGrey;
            EditorGUILayout.LabelField(text, p);
        }

        public void Paragraph(string text, Color color)
        {
            p.normal.textColor = color;
            EditorGUILayout.LabelField(text, p);
            p.normal.textColor = DarkGrey;
        }

        public void Paragraph(string[] text)
        {
            p.normal.textColor = DarkGrey;
            int ix = GetRealIndex(text.Length, LanIndex);
            EditorGUILayout.LabelField(text[ix], p);
        }

        public void Paragraph(string[] text, Color color)
        {
            p.normal.textColor = color;
            int ix = GetRealIndex(text.Length, LanIndex);
            EditorGUILayout.LabelField(text[ix], p);
            p.normal.textColor = DarkGrey;
        }
        #endregion

        #region Space
        public void Space()
        {
            EditorGUILayout.Space(); EditorGUILayout.Space();
        }

        public void Space(int time)
        {
            for(int i = 0; i < time; i++)
            {
                EditorGUILayout.Space();
            }
        }
        #endregion

        #region WebLink
        public void WebLink(string text, string hyperlink)
        {
            if (GUILayout.Button(text))
            {
                Application.OpenURL(hyperlink);
            }
        }

        public void WebLink(string[] text, string hyperlink)
        {
            int ix = GetRealIndex(text.Length, LanIndex);
            if (GUILayout.Button(text[ix]))
            {
                Application.OpenURL(hyperlink);
            }
        }

        public void WebLink(string text, string hyperlink, float linkWidth)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(text, GUILayout.Width(linkWidth)))
            {
                Application.OpenURL(hyperlink);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public void WebLink(string[] text, string hyperlink, float linkWidth)
        {
            int ix = GetRealIndex(text.Length, LanIndex);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(text[ix], GUILayout.Width(linkWidth)))
            {
                Application.OpenURL(hyperlink);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        #endregion

        #region Link
        public void Link(string text, string hyperlink)
        {
            if (GUILayout.Button(text))
            {
                DocsEditor.Instance.ChangePage(hyperlink);
            }
        }

        public void Link(string[] text, string hyperlink)
        {
            int ix = GetRealIndex(text.Length, LanIndex);
            if (GUILayout.Button(text[ix]))
            {
                DocsEditor.Instance.ChangePage(hyperlink);
            }
        }

        public void Link(string text, string hyperlink, float linkWidth)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(text, GUILayout.Width(linkWidth)))
            {
                DocsEditor.Instance.ChangePage(hyperlink);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public void Link(string[] text, string hyperlink, float linkWidth)
        {
            int ix = GetRealIndex(text.Length, LanIndex);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(text[ix], GUILayout.Width(linkWidth)))
            {
                DocsEditor.Instance.ChangePage(hyperlink);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        #endregion

        #region Titles
        public void Title(string text)
        {
            EditorGUILayout.LabelField(text, title);
        }

        public void Title(string[] text)
        {
            int ix = GetRealIndex(text.Length, LanIndex);
            EditorGUILayout.LabelField(text[ix], title);
        }

        public void Title2(string text)
        {
            EditorGUILayout.LabelField(text, title2);
        }

        public void Title2(string[] text)
        {
            int ix = GetRealIndex(text.Length, LanIndex);
            EditorGUILayout.LabelField(text[ix], title2);
        }

        public void Title3(string text)
        {
            EditorGUILayout.LabelField(text, title3);
        }

        public void Title3(string[] text)
        {
            int ix = GetRealIndex(text.Length, LanIndex);
            EditorGUILayout.LabelField(text[ix], title3);
        }
        #endregion

        #region Image
        public void Image(Texture2D tex)
        {
            GUILayout.Box(tex, GUILayout.ExpandWidth(true));
        }

        public void Image(Texture2D tex, float size)
        {
            GUILayout.Box(tex, GUILayout.Height(size), GUILayout.ExpandWidth(true));
        }

        public void Image(string path)
        {
            Image(AssetDatabase.LoadAssetAtPath<Texture2D>(path));
        }

        public void Image(string path, float size)
        {
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            Image(tex, size);
        }
        #endregion
    }
}
#endif
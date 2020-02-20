#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    [CustomEditor(typeof(EPathFollow))]
    [CanEditMultipleObjects]
    public class EPathFollowEditor : Editor
    {
        EPathFollow pf;

        public override void OnInspectorGUI()
        {
            if (!pf) pf = (EPathFollow)target;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("DrawPoint"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Smooth"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("SmoothTime"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("targets"), true);
            serializedObject.ApplyModifiedProperties();
        }

        private void OnSceneGUI()
        {
            if (!pf) pf = (EPathFollow)target;

            Handles.BeginGUI();
            if (GUILayout.Button("Add", GUILayout.Width(200)))
            {
                List<Vector3> buffer = pf.path.ToList();

                if (buffer.Count >= 2)
                {
                    Vector3 dir = buffer[buffer.Count - 1] - buffer[buffer.Count - 2];
                    buffer.Add(buffer[buffer.Count - 1]);
                    buffer[buffer.Count - 1] += dir.normalized;
                }
                else if (buffer.Count == 0)
                {
                    buffer.Add(Vector3.zero);
                }
                else
                {
                    buffer.Add(buffer[buffer.Count - 1]);
                    buffer[buffer.Count - 1] += Vector3.up;
                }

                pf.path = buffer.ToArray();
            }
            if (GUILayout.Button("Delete", GUILayout.Width(200)))
            {
                List<Vector3> buffer = pf.path.ToList();
                buffer.RemoveAt(buffer.Count - 1);
                pf.path = buffer.ToArray();
            }
            if (GUILayout.Button("Clear", GUILayout.Width(200)))
            {
                pf.path = new Vector3[0];
            }
            Handles.EndGUI();

            EditorGUI.BeginChangeCheck();
            for(int i = 1; i < pf.path.Length; i++)
            {
                Vector3 v = Handles.PositionHandle(pf.transform.TransformPoint(pf.path[i]), Quaternion.identity);
                pf.path[i] = pf.transform.InverseTransformPoint(v);
            }

            if (EditorGUI.EndChangeCheck())
            {

            }
        }
    }
}
#endif
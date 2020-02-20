#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace ETool
{
    [CustomEditor(typeof(ETestDistance))]
    public class ETestDistanceEditor : Editor
    {
        private void OnSceneGUI()
        {
            ETestDistance td = (ETestDistance)target;

            if(td.target != null)
            {

                GUIStyle style = new GUIStyle();
                style.fontSize = 20;
                Handles.Label(
                    (td.transform.position + td.target.position) / 2,
                    "Distance: " + Vector3.Distance(td.transform.position, td.target.position).ToString(),
                    style);
            }
        }
    }
}
#endif
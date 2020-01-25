using System;
using UnityEditor;
using UnityEngine;

namespace ETool
{
    [System.Serializable]
    public class Connection
    {
        // node index, field index
        public Vector2Int inPointMark;
        public Vector2Int outPointMark;
        public int page;

        private Vector2 mousePosition;
        private bool onConnectType;
        public FieldType fieldType = FieldType.Event;

        public Connection(Vector2Int inPointMark, Vector2Int outPointMark, bool onConnectionType)
        {
            this.inPointMark = inPointMark;
            this.outPointMark = outPointMark;
            onConnectType = onConnectionType;
        }

        public void UpdateP(Vector2 mousePosition)
        {
            this.mousePosition = mousePosition;
        }

        public void Draw()
        {
            ConnectionPoint outPoint = NodeBasedEditor.Instance.GetConnectionPoint(outPointMark, false);

            if (!onConnectType)
            {
                ConnectionPoint inPoint = NodeBasedEditor.Instance.GetConnectionPoint(inPointMark, true);

                if (inPoint == null) Debug.Log("In null");
                if (outPoint == null) Debug.Log("Out null");

                Handles.DrawBezier(
                    inPoint.rect.center,
                    outPoint.rect.center,
                    inPoint.rect.center + Vector2.left * 50f,
                    outPoint.rect.center - Vector2.left * 50f,
                    Field.GetColorByFieldType(fieldType, 1.0f),
                    null,
                    5f
                );

                if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 10, 8, Handles.CircleHandleCap))
                {
                    NodeBasedEditor.Instance.OnClickRemoveConnection(this);
                }
            }

            else
            {
                Handles.DrawBezier(
                        mousePosition,
                        outPoint.rect.center,
                        mousePosition,
                        outPoint.rect.center - Vector2.left * 50f,
                        Color.white,
                        null,
                        2f
                    );
            }
        }
    }
}
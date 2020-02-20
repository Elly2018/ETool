using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ETool
{
    /// <summary>
    /// Define a connection data <br />
    /// </summary>
    [System.Serializable]
    public class Connection
    {
        public const float Selected_Width = 12f;
        public const float UnSelected_Width = 5f;

        /// <summary>
        /// Input data mark <br />
        /// x: Node index <br />
        /// y: Field index <br />
        /// </summary>
        public Vector2Int inPointMark;

        /// <summary>
        /// Output data mark <br />
        /// x: Node index <br />
        /// y: Field index <br />
        /// </summary>
        public Vector2Int outPointMark;

        /// <summary>
        /// Index of page
        /// </summary>
        public int page;

        /// <summary>
        /// Define the select state
        /// </summary>
        public bool isSelected = false;

        /// <summary>
        /// What field type this connection is
        /// </summary>
        public FieldType fieldType = FieldType.Event;

        /// <summary>
        /// Local variable specifie mouse position
        /// </summary>
        private Vector2 mousePosition;

        private bool onConnectType;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inPointMark">Input Mark</param>
        /// <param name="outPointMark">Output Mark</param>
        /// <param name="onConnectionType">Define connection type</param>
        public Connection(Vector2Int inPointMark, Vector2Int outPointMark, bool onConnectionType)
        {
            this.inPointMark = inPointMark;
            this.outPointMark = outPointMark;
            onConnectType = onConnectionType;
        }

        /// <summary>
        /// Change local mouse position variable
        /// </summary>
        /// <param name="mousePosition">Mouse Position</param>
        public void UpdateMousePosition(Vector2 mousePosition)
        {
            this.mousePosition = mousePosition;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Drawing method
        /// </summary>
        public void Draw()
        {
            /* Get connection point output */
            ConnectionPoint outPoint = NodeBasedEditor.Instance.Connection_GetConnectionPoint(outPointMark, false);

            /* If it's node to node connection */
            if (!onConnectType)
            {
                /* Get connection point input */
                ConnectionPoint inPoint = NodeBasedEditor.Instance.Connection_GetConnectionPoint(inPointMark, true);

                /* Debug, if any of mark is null, tell the developer */
                /* Usually it should not be null */
                if (inPoint == null) Debug.Log("In null");
                if (outPoint == null) Debug.Log("Out null");

                /* Drawing bezier curve */
                Handles.DrawBezier(
                    inPoint.rect.center, // Start point
                    outPoint.rect.center, // End point
                    inPoint.rect.center + Vector2.left * 150f, // Start tangent
                    outPoint.rect.center - Vector2.left * 150f, // End tangent
                    Field.GetColorByFieldType(fieldType, 1.0f), // Color
                    null, // Texture
                    isSelected ? Selected_Width : UnSelected_Width // Width
                );

                /* Create curve center delete button */
                if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 10, 8, Handles.CircleHandleCap))
                {
                    /* When click the button, tell editor delete this connection */
                    //NodeBasedEditor.Instance.OnClickRemoveConnection(this);
                    isSelected = !isSelected;
                }
            }
            /* If it's on connect connection */
            else
            {
                /* Drawing bezier curve */
                Handles.DrawBezier(
                        mousePosition, // Start point
                        outPoint.rect.center, // End point
                        mousePosition, // Start tangent
                        outPoint.rect.center - Vector2.left * 50f, // End tangent
                        Color.white, // Color
                        null, // Texture
                        2f // Width
                    );
            }
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    {
                        if (!e.shift && !NodeBasedEditor.Instance.Check_AnyOtherNodeAreSelected(null) && e.button == 0)
                        {
                            isSelected = false;
                        }
                        break;
                    }
            }
            return true;
        }

        public void ProcessContextMenu() 
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Delete Selected Connection"), false, null);
            genericMenu.ShowAsContext();
        }
#endif
    }
}
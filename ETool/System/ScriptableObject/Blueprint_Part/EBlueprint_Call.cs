using ETool.ANode;
using UnityEngine;

namespace ETool
{
    public partial class EBlueprint : ScriptableObject
    {
        public void EOnValidate(GameObject g, BlueprintGameobjectRegister[] b)
        {
            nodes = InitializeBlueprint(nodes, blueprintVariables, blueprintEvent.customEvent, Inherit);
        }

        public void EStart(GameObject g, BlueprintGameobjectRegister[] b)
        {
            Node c = GetNode(typeof(AConstructor), _InputInstance);
            Node a = GetNode(typeof(AStart), _InputInstance);

            if (c != null)
                c.ProcessCalling(_InputInstance);
            if (a != null)
                a.ProcessCalling(_InputInstance);
        }

        public void EStop(GameObject g, BlueprintGameobjectRegister[] b)
        {
        }

        public void EUpdate(GameObject g, BlueprintGameobjectRegister[] b)
        {
            Node a = GetNode(typeof(AUpdate), _InputInstance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EFixedUpdate(GameObject g, BlueprintGameobjectRegister[] b)
        {
            Node a = GetNode(typeof(AFixedUpdate), _InputInstance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void ELateUpdate(GameObject g, BlueprintGameobjectRegister[] b)
        {
            Node a = GetNode(typeof(ALateUpdate), _InputInstance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EOnDestroy(GameObject g, BlueprintGameobjectRegister[] b)
        {
            Node a = GetNode(typeof(AOnDestory), _InputInstance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EOnCollisionEnter(Collision collision)
        {
            _InputInstance.m_Collision = collision;
            Node a = GetNode(typeof(AOnCollisionEnter), _InputInstance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EOnCollisionExit(Collision collision)
        {
            _InputInstance.m_Collision = collision;
            Node a = GetNode(typeof(AOnCollisionExit), _InputInstance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EOnCollisionStay(Collision collision)
        {
            _InputInstance.m_Collision = collision;
            Node a = GetNode(typeof(AOnCollisionStay), _InputInstance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EOnCollisionEnter2D(Collision2D collision)
        {
            _InputInstance.m_Collision2D = collision;
        }

        public void EOnCollisionExit2D(Collision2D collision)
        {
            _InputInstance.m_Collision2D = collision;
        }

        public void EOnCollisionStay2D(Collision2D collision)
        {
            _InputInstance.m_Collision2D = collision;
        }

        public void EOnTriggerEnter(Collider other)
        {
            _InputInstance.m_Collider = other;
        }

        public void EOnTriggerExit(Collider other)
        {
            _InputInstance.m_Collider = other;
        }

        public void EOnTriggerStay(Collider other)
        {
            _InputInstance.m_Collider = other;
        }

        public void EOnTriggerEnter2D(Collider2D collision)
        {
            _InputInstance.m_Collider2D = collision;
        }

        public void EOnTriggerExit2D(Collider2D collision)
        {
            _InputInstance.m_Collider2D = collision;
        }

        public void EOnTriggerStay2D(Collider2D collision)
        {
            _InputInstance.m_Collider2D = collision;
        }

        private void OnEnable()
        {
            bool exist = false;
            foreach(var i in nodes)
            {
                if (i.NodeType == typeof(AConstructor).FullName) exist = true;
            }

            if (!exist)
            {
                Node_AddNode(Vector2.zero, 1, typeof(AConstructor));
            }
        }
    }
}

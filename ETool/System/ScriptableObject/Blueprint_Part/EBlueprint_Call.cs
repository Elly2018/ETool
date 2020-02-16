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
            //InitializeProperties(g, b);

            Node c = GetNode(typeof(AConstructor), nodes_instance);
            Node a = GetNode(typeof(AStart), nodes_instance);

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
            Node a = GetNode(typeof(AUpdate), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EFixedUpdate(GameObject g, BlueprintGameobjectRegister[] b)
        {
            Node a = GetNode(typeof(AFixedUpdate), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void ELateUpdate(GameObject g, BlueprintGameobjectRegister[] b)
        {
            Node a = GetNode(typeof(ALateUpdate), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EOnDestroy(GameObject g, BlueprintGameobjectRegister[] b)
        {
            Node a = GetNode(typeof(AOnDestory), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EOnCollisionEnter(Collision collision)
        {
            _InputInstance.m_Collision = collision;
            Node a = GetNode(typeof(AOnCollisionEnter), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EOnCollisionExit(Collision collision)
        {
            _InputInstance.m_Collision = collision;
            Node a = GetNode(typeof(AOnCollisionExit), nodes_instance);
            if (a != null)
            {
                a.ProcessCalling(_InputInstance);
            }
        }

        public void EOnCollisionStay(Collision collision)
        {
            _InputInstance.m_Collision = collision;
            Node a = GetNode(typeof(AOnCollisionStay), nodes_instance);
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
    }
}

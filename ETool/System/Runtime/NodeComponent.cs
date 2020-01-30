using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ETool
{
    [AddComponentMenu("ETool/Node Component")]
    public class NodeComponent : MonoBehaviour
    {
        public EBlueprint ABlueprint;
        public BlueprintGameobjectRegister[] blueprintGameobjectRegister;

        private void Awake()
        {
            EBlueprint buffer = ScriptableObject.CreateInstance<EBlueprint>();
            buffer.EBlueprintClone(ABlueprint, gameObject, blueprintGameobjectRegister);
            buffer.name = ABlueprint.name + "(Instance)";
            ABlueprint = buffer;
        }

        private void OnValidate()
        {
            if (ABlueprint == null) return;
            Calling("EOnValidate");
        }

        private void Start()
        {
            if (ABlueprint == null) return;
            Calling("EStart");
        }

        private void Update()
        {
            if (ABlueprint == null) return;
            Calling("EUpdate");
        }

        private void FixedUpdate()
        {
            if (ABlueprint == null) return;
            Calling("EFixedUpdate");
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                if (ABlueprint == null) return;
                Calling("EOnValidate");
            }
            if (ABlueprint == null) return;
            Calling("EStop");
        }
        
        private void OnDestroy()
        {
            if (ABlueprint == null) return;
            Calling("EOnDestroy");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (ABlueprint == null) return;
            PCalling("EOnCollisionEnter", collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (ABlueprint == null) return;
            PCalling("EOnCollisionExit", collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (ABlueprint == null) return;
            PCalling("EOnCollisionStay", collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (ABlueprint == null) return;
            PCalling("EOnCollisionEnter2D", collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (ABlueprint == null) return;
            PCalling("EOnCollisionExit2D", collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (ABlueprint == null) return;
            PCalling("EOnCollisionStay2D", collision);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ABlueprint == null) return;
            PCalling("EOnTriggerEnter", other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (ABlueprint == null) return;
            PCalling("EOnTriggerExit", other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (ABlueprint == null) return;
            PCalling("EOnTriggerStay", other);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (ABlueprint == null) return;
            PCalling("EOnTriggerEnter2D", collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (ABlueprint == null) return;
            PCalling("EOnTriggerExit2D", collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (ABlueprint == null) return;
            PCalling("EOnTriggerStay2D", collision);
        }

        private void Calling(string methodName)
        {
            Type blueprint = ABlueprint.GetType();
            MethodInfo info = blueprint.GetMethod(methodName);
            List<object> _arg = new List<object>();
            _arg.Add(gameObject);
            _arg.Add(blueprintGameobjectRegister);
            info.Invoke(ABlueprint, _arg.ToArray());
        }

        private void PCalling(string methodName, object o)
        {
            Type blueprint = ABlueprint.GetType();
            MethodInfo info = blueprint.GetMethod(methodName);
            List<object> _arg = new List<object>();
            _arg.Add(o);
            info.Invoke(ABlueprint, _arg.ToArray());
        }
    }

    [System.Serializable]
    public class BlueprintGameobjectRegister
    {
        public string label;
        public GameObject target;
    }
}
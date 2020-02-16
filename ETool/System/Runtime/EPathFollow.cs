using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETool
{
    [ExecuteAlways]
    [AddComponentMenu("ETool/Path Follow")]
    public class EPathFollow : MonoBehaviour
    {
        [System.Serializable]
        public class PathFollowElement
        {
            public Transform target;
            [Range(0, 1)] public float fac;
            public Vector3 offset;
            [HideInInspector] public Vector3 velocity;
        }

        [SerializeField] public Vector3[] path;
        [SerializeField] public List<PathFollowElement> targets = new List<PathFollowElement>();
        [SerializeField] public float Length
        {
            get
            {
                float[] length = GetAllDistance();
                float result = 0;
                for(int i = 0; i < length.Length; i++)
                {
                    result += length[i];
                }
                return result;
            }
        }
        [SerializeField] public bool DrawPoint;
        [SerializeField] public bool Smooth;
        [SerializeField] public float SmoothTime;

        public void AddObjectToPath(Transform t, float fac)
        {
            targets.Add(new PathFollowElement() { target = t, fac = fac});
        }

        public void RemoveObjectFromPathRegister(Transform t)
        {
            foreach(var i in targets)
            {
                if (i.target == t)
                {
                    targets.Remove(i);
                    return;
                }
            }
        }

        public void RemoveAllObjectFromPathRegister()
        {
            targets.Clear();
        }

        private void Update()
        {
            foreach (var i in targets)
            {

                if (i.target != null)
                {
                    if (!Smooth || !Application.isPlaying)
                    {
                        i.target.position = transform.TransformPoint(GetPosByFac(i.fac) + i.offset);
                    }
                    else
                    {
                        i.target.position = Vector3.SmoothDamp(i.target.position, transform.TransformPoint(GetPosByFac(i.fac) + i.offset), ref i.velocity, SmoothTime);
                    }
                }
            }
        }

        private Vector3 GetPosByFac(float fac)
        {
            float[] total = GetAllDistance();
            float dif = fac * Length;
            for(int i = 0; i < total.Length; i++)
            {
                if(dif > total[i])
                {
                    dif -= total[i];
                }
                else
                {
                    Vector3 totalvector = transform.TransformPoint(path[i + 1]) - transform.TransformPoint(path[i]);
                    return path[i] + (totalvector * (dif / total[i]));
                }
            }
            return Vector3.zero;
        }

        private float[] GetAllDistance()
        {
            if (path.Length >= 2)
            {
                float[] result = new float[path.Length - 1];
                for (int i = 0; i < path.Length - 1; i++)
                {
                    result[i] = Vector3.Distance(transform.TransformPoint(path[i]), transform.TransformPoint(path[i + 1]));
                }
                return result;
            }
            return new float[] { 0 };
        }

        private void OnDrawGizmosSelected()
        {
            if(path.Length >= 2)
            {
                for(int i = 0; i < path.Length - 1; i++)
                {
                    Gizmos.DrawLine(transform.TransformPoint(path[i]), transform.TransformPoint(path[i + 1]));
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (DrawPoint)
            {
                for(int i = 0; i < path.Length; i++)
                {
                    Gizmos.DrawWireSphere(transform.TransformPoint(path[i]), 0.5f);
                }
            }
        }
    }
}
using UnityEngine;

namespace ETool
{
    [AddComponentMenu("ETool/Utility/Test Distance")]
    public class ETestDistance : MonoBehaviour
    {
        public Transform target;

        private void OnDrawGizmosSelected()
        {
            if(target != null)
            {
                Gizmos.DrawLine(transform.position, target.position);
            }
        }
    }
}

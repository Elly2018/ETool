using UnityEngine;

namespace ETool
{
    [AddComponentMenu("ETool/Utility/Test Radius")]
    public class ETestRadius : MonoBehaviour
    {
        public float target;

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(target, 0, 0));
            Gizmos.DrawWireSphere(transform.position, target);
        }
    }
}

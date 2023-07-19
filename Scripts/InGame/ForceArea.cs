using UnityEngine;
using Utils;

namespace Unity1week202306.InGame
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Unity1week202306/ForceArea")]
    public class ForceArea : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _direction;

        [SerializeField]
        private float _power = 1;

        public Vector3 Direction => _direction;
        public float Power => _power;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            GizmosExtensions.DrawArrow(transform.position, transform.position + _direction);
        }

        private void OnValidate()
        {
            if (TryGetComponent<Collider2D>(out var collider))
            {
                collider.isTrigger = true;
            }
        }
    }
}
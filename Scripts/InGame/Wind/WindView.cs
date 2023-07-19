using UnityEngine;

namespace Unity1week202306.InGame.Wind
{
    public class WindView : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystemForceField _forceField;
        
        public void SetDirection(Vector2 vector)
        {
            _forceField.directionX = vector.x;
            _forceField.directionY = vector.y;
        }
    }
}
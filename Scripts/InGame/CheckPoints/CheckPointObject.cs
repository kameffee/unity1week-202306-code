using UnityEngine;

namespace Unity1week202306.InGame.CheckPoints
{
    public class CheckPointObject : MonoBehaviour
    {
        [SerializeField]
        private int _id;

        public int Id => _id;

        public Vector3 WorldPosition => transform.position;
        
        public CheckPoint ToStartPoint() => new(new CheckPointIdentifier(_id), transform.position);

        private void OnValidate()
        {
            if (_id < 0)
            {
                Debug.LogError($"idが0未満です: {_id}");
            }
        }
    }
}
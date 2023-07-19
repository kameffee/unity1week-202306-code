using UniRx;
using UnityEngine;

namespace Unity1week202306.InGame.Umbrella
{
    public class UmbrellaController : MonoBehaviour
    {
        [SerializeField]
        private Transform _atLookTarget;

        [SerializeField]
        private GameObject _openedUmbrella;

        [SerializeField]
        private GameObject _closedUmbrella;

        /// <summary>
        /// 傘の向き
        /// </summary>
        public Vector2 Direction { get; private set; } = Vector2.zero;

        public IReadOnlyReactiveProperty<bool> IsOpened => _isOpened;
        
        public bool IsActivate => _isActivate;

        private Camera _mainCamera;
        private BoolReactiveProperty _isOpened = new(false);
        private bool _isActivate = true;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void Activate(bool isActivate)
        {
            _isActivate = isActivate;
        }

        public void Open()
        {
            _isOpened.Value = true;
            _openedUmbrella.SetActive(true);
            _closedUmbrella.SetActive(false);
        }

        public void Close()
        {
            _isOpened.Value = false;
            _openedUmbrella.SetActive(false);
            _closedUmbrella.SetActive(true);
        }

        public void Switch()
        {
            if (_isOpened.Value)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        public void SetDirection(Vector2 direction)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            Direction = direction;
        }

        public void SetDirection(float angle)
        {
            // 角度をラジアンに変換
            float angleInRadians = angle * Mathf.Deg2Rad;

            // ラジアンを使用してベクトルを計算
            float x = Mathf.Cos(angleInRadians);
            float y = Mathf.Sin(angleInRadians);

            SetDirection(new Vector2(x, y));
        }
    }
}
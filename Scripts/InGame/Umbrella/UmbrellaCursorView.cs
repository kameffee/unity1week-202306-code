using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202306.InGame.Umbrella
{
    public class UmbrellaCursorView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        [Header("Renderer")]
        [SerializeField]
        private Image _openedImage;
        
        [SerializeField]
        private Image _closedImage;


        [Header("Animation")]
        [SerializeField]
        private float _defaultShowDuration = 0.3f;

        [SerializeField]
        private float _defaultHideDuration = 0.3f;

        public Vector2 Direction => _direction;

        private Vector2 _direction;

        private void Start()
        {
            Hide(0).Forget();
        }

        public async UniTask Show(float? duration = null, CancellationToken cancellationToken = default)
        {
            var actualDuration = duration ?? _defaultShowDuration;
            var doFade = _canvasGroup.DOFade(1f, actualDuration)
                .SetEase(Ease.Linear);

            doFade.SetLink(gameObject);
            doFade.WithCancellation(cancellationToken);
            await doFade.Play();
        }

        public async UniTask Hide(float? duration = null, CancellationToken cancellationToken = default)
        {
            var actualDuration = duration ?? _defaultHideDuration;
            var doFade = _canvasGroup.DOFade(0f, actualDuration)
                .SetEase(Ease.Linear);

            doFade.SetLink(gameObject);
            doFade.WithCancellation(cancellationToken);
            await doFade.Play();
        }

        private void Update()
        {
            var mousePosition = Input.mousePosition;
            var screenCenterPosition = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);

            var end = mousePosition - screenCenterPosition;
            _direction = end.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
        }
        
        public void SetOpened(bool isOpened)
        {
            _openedImage.enabled = isOpened;
            _closedImage.enabled = !isOpened;
        }
    }
}
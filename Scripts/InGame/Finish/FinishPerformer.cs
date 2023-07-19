using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using Unity1week202306.InGame.Players;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202306.InGame.Finish
{
    public class FinishPerformer : MonoBehaviour
    {
        [SerializeField]
        private Transform _toPosition;

        [SerializeField]
        private CanvasGroup _thanksMessageCanvasGroup;

        [SerializeField]
        private float _baseSpeed = 1f;

        [SerializeField]
        private GameObject _endingCamera;

        [SerializeField]
        private Button _titleButton;

        private void Awake()
        {
            _thanksMessageCanvasGroup.DOFade(0f, 0f);
        }

        public async UniTask Play(PlayerController target, CancellationToken cancellationToken = default)
        {
            _endingCamera.SetActive(true);

            var rb = target.GetComponent<Rigidbody2D>();

            await rb
                .DOMove(_toPosition.position, _baseSpeed)
                .SetSpeedBased(true);

            rb
                .DOMoveY(0.5f, 5)
                .SetRelative(true)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public async UniTask ShowThanksMessage(CancellationToken cancellationToken = default)
        {
            var tween = _thanksMessageCanvasGroup.DOFade(1f, 1f);
            tween.WithCancellation(cancellationToken);
        }

        public void ShowTitleButton()
        {
            _titleButton.gameObject.SetActive(true);
        }

        public IObservable<Unit> OnClickTitleButtonAsObservable()
        {
            return _titleButton.OnClickAsObservable();
        }
    }
}
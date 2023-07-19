using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Unity1week202306.LetterBox
{
    public class LetterBoxView : MonoBehaviour
    {
        [SerializeField]
        private float _defaultShowDuration = 1f;

        [SerializeField]
        private float _defaultHideDuration = 1f;

        [SerializeField]
        private RectTransform _letterBoxTop;

        [SerializeField]
        private RectTransform _letterBoxBottom;

        private void Awake()
        {
            _letterBoxTop.DOAnchorPosY(130f, 0);
            _letterBoxBottom.DOAnchorPosY(-130f, 0);
        }

        public async UniTask ShowAsync(float? duration = null, CancellationToken cancellationToken = default)
        {
            var durationValue = duration ?? _defaultShowDuration;

            // 上下のレターボックスを表示
            var sequence = DOTween.Sequence();
            sequence.Append(_letterBoxTop
                .DOAnchorPosY(0f, durationValue)
                .SetEase(Ease.OutSine)
            );
            sequence.Join(_letterBoxBottom
                .DOAnchorPosY(0f, durationValue)
                .SetEase(Ease.OutSine)
            );
            sequence.WithCancellation(cancellationToken);
            sequence.SetLink(gameObject);

            await sequence.Play();
        }

        public async UniTask HideAsync(float? duration = null, CancellationToken cancellationToken = default)
        {
            var durationValue = duration ?? _defaultHideDuration;

            var sequence = DOTween.Sequence();
            sequence.Append(_letterBoxTop
                .DOAnchorPosY(130f, durationValue)
                .SetEase(Ease.InSine)
            );
            sequence.Join(_letterBoxBottom
                .DOAnchorPosY(-130f, durationValue)
                .SetEase(Ease.OutSine)
            );

            sequence.WithCancellation(cancellationToken);
            sequence.SetLink(gameObject);

            await sequence.Play();
        }
    }
}
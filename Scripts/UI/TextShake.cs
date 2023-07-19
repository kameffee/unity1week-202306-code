using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Unity1week202306.UI
{
    public class TextShake : MonoBehaviour
    {
        [SerializeField]
        private float _shakeStrength = 1f;
        
        [SerializeField]
        private float _shakeDuration = 1f;
        
        [SerializeField]
        private int _shakeVibrato = 2;
        
        [SerializeField]
        private TextMeshProUGUI _messageText;

        private readonly List<Tween> _tweenList = new();
        
        private void Start()
        {
            Shake();
        }


        public void Shake()
        {
            DOTweenTMPAnimator tmproAnimator = new DOTweenTMPAnimator(_messageText);

            for (int i = 0; i < tmproAnimator.textInfo.characterCount; ++i)
            {
                // 揺らす
                _tweenList.Add(tmproAnimator.DOShakeCharOffset(i, _shakeDuration, _shakeStrength, _shakeVibrato, fadeOut: false)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.Linear));
            }
        }

        public void FadeOut()
        {
            _tweenList.ForEach(tween => tween?.Kill());

            DOTweenTMPAnimator tmproAnimator = new DOTweenTMPAnimator(_messageText);

            // 
            for (int i = 0; i < tmproAnimator.textInfo.characterCount; ++i)
            {
                // 登場開始アニメ
                _tweenList.Add(DOTween.Sequence()
                    .Append(tmproAnimator.DOFadeChar(i, 0, 1f).SetEase(Ease.OutSine))
                    .SetDelay(Random.Range(0.1f, 1.0f)).Play());

                // 揺らす
                _tweenList.Add(tmproAnimator.DOShakeCharOffset(i, 1f, 1, 2)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear));
            }
        }
    }
}
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Unity1week202306.Title
{
    public class TitleTextView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        public async UniTask Show()
        {
            await _canvasGroup.DOFade(1, 0.5f);
        }

        public async UniTask Hide()
        {
            await _canvasGroup.DOFade(0, 0.5f);
        }
    }
}
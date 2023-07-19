using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202306.Title
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _licenseButton;

        public IObservable<Unit> OnClickStartButtonAsObservable() => _startButton.OnClickAsObservable();

        public IObservable<Unit> OnClickLicenseButtonAsObservable() => _licenseButton.OnClickAsObservable();

        private void Awake()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public async UniTask Show()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            await _canvasGroup.DOFade(1, 0.5f);
        }

        public async UniTask Hide()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            await _canvasGroup.DOFade(0, 0.5f);
        }
    }
}
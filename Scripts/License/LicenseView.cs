using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202306.License
{
    public class LicenseView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _root;
        
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private TextMeshProUGUI _licenseText;

        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private RectTransform _toPosition;
        
        [SerializeField]
        private RectTransform _fromPosition;

        public IObservable<Unit> OnCloseAsObservable() => _closeButton.OnClickAsObservable();

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
            _root.anchoredPosition = _fromPosition.anchoredPosition;
            _root.DOAnchorPos(_toPosition.anchoredPosition, 0.3f)
                .SetEase(Ease.OutBack);
            
            await _canvasGroup.DOFade(1, 0.2f);
        }

        public async UniTask Hide()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            await _canvasGroup.DOFade(0, 0.2f);
        }

        public void SetLicenseText(string licenseData)
        {
            _licenseText.text = licenseData;
        }
    }
}
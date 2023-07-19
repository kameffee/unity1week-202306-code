using DG.Tweening;
using Unity1week202306.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Unity1week202306.UI
{
    public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Hover")]
        [SerializeField]
        private float _hoverScalingRatio = 1.05f;

        [SerializeField]
        private float _hoverScalingTime = 0.1f;

        [Header("Down")]
        [SerializeField]
        private float _downScalingRatio = 0.95f;

        [SerializeField]
        private float _downScalingTime = 0.1f;

        private RectTransform _rectTransform;

        [Inject]
        private AudioPlayer _audioPlayer;

        private void Start()
        {
            _rectTransform = transform as RectTransform;

            var lifetimeScope = LifetimeScope.Find<LifetimeScope>(gameObject.scene);
            lifetimeScope.Container.Inject(this);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            _rectTransform.DOScale(Vector3.one * _hoverScalingRatio, _hoverScalingTime);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _rectTransform.DOScale(Vector3.one * _downScalingRatio, _downScalingTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _rectTransform.DOScale(Vector3.one, _hoverScalingTime);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _rectTransform.DOScale(Vector3.one * _hoverScalingRatio, _hoverScalingTime);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _audioPlayer.PlaySe("ClickButton");
        }
    }
}
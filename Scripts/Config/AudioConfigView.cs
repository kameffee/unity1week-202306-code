using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using Unity1week202306.Audio;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Unity1week202306.Config
{
    public class AudioConfigView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Slider _bgmVolumeSlider;

        [SerializeField]
        private Slider _seVolumeSlider;

        [Inject]
        private readonly AudioPlayer _audioPlayer;

        public IObservable<float> OnChangedBgmVolumeAsObservable() => _bgmVolumeSlider.OnValueChangedAsObservable();
        public IObservable<float> OnChangedSeVolumeAsObservable() => _seVolumeSlider.OnValueChangedAsObservable();

        private void Awake()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0;
        }

        private void Start()
        {
            var lifetimeScope = LifetimeScope.Find<LifetimeScope>(gameObject.scene);
            lifetimeScope.Container.Inject(this);

            _seVolumeSlider.OnPointerUpAsObservable()
                .Subscribe(_ => _audioPlayer.PlaySe("OnChangedSeVolume"))
                .AddTo(this);
        }

        public void SetBgmVolume(float volume)
        {
            _bgmVolumeSlider.value = volume;
        }

        public void SetSeVolume(float volume)
        {
            _seVolumeSlider.value = volume;
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
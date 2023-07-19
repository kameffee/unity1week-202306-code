using Cysharp.Threading.Tasks;
using UniRx;
using Unity1week202306.Audio;
using VContainer.Unity;

namespace Unity1week202306.Config
{
    public class ConfigPresenter : IInitializable
    {
        private AudioSettingsService _settingsService;
        private readonly AudioConfigView _audioConfigView;
        private readonly CompositeDisposable _disposable = new();

        public ConfigPresenter(
            AudioConfigView audioConfigView,
            AudioSettingsService settingsService)
        {
            _audioConfigView = audioConfigView;
            _settingsService = settingsService;
        }

        public void Initialize()
        {
            _settingsService.BgmVolume
                .Subscribe(volume => _audioConfigView.SetBgmVolume(volume.Value))
                .AddTo(_disposable);

            _settingsService.SeVolume
                .Subscribe(volume => _audioConfigView.SetSeVolume(volume.Value))
                .AddTo(_disposable);

            _audioConfigView.OnChangedBgmVolumeAsObservable()
                .Subscribe(value => _settingsService.SetBgmVolume(new AudioVolume(value)))
                .AddTo(_disposable);

            _audioConfigView.OnChangedSeVolumeAsObservable()
                .Subscribe(value => _settingsService.SetSeVolume(new AudioVolume(value)))
                .AddTo(_disposable);
        }

        public async UniTask Show()
        {
            await _audioConfigView.Show();
        }

        public async UniTask Hide()
        {
            await _audioConfigView.Hide();
        }
    }
}
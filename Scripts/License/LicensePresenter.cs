using System;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer.Unity;

namespace Unity1week202306.License
{
    public class LicensePresenter : IInitializable, IDisposable
    {
        private readonly LicenseView _view;
        private readonly GetLicenseTextUseCase _getLicenseTextUseCase;

        private readonly CompositeDisposable _disposable = new();

        public LicensePresenter(
            LicenseView view,
            GetLicenseTextUseCase getLicenseTextUseCase)
        {
            _view = view;
            _getLicenseTextUseCase = getLicenseTextUseCase;
        }

        public void Initialize()
        {
            _getLicenseTextUseCase.Get().ToObservable()
                .Subscribe(licenseText => _view.SetLicenseText(licenseText))
                .AddTo(_disposable);

            _view.OnCloseAsObservable()
                .Subscribe(_ => _view.Hide().Forget())
                .AddTo(_disposable);
        }

        public async UniTask Show()
        {
            await _view.Show();
        }

        public async UniTask Hide()
        {
            await _view.Hide();
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
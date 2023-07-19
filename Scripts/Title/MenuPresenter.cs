using System;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity1week202306.License;
using Unity1week202306.Scenes;
using VContainer.Unity;

namespace Unity1week202306.Title
{
    public class MenuPresenter : IInitializable, IDisposable
    {
        private readonly MenuView _menuView;
        private readonly SceneLoader _sceneLoader;
        private readonly LicensePresenter _licensePresenter;

        private readonly CompositeDisposable _disposable = new();

        public MenuPresenter(
            MenuView menuView,
            SceneLoader sceneLoader,
            LicensePresenter licensePresenter)
        {
            _menuView = menuView;
            _sceneLoader = sceneLoader;
            _licensePresenter = licensePresenter;
        }

        public void Initialize()
        {
            _menuView.OnClickStartButtonAsObservable()
                .SelectMany(_ => _sceneLoader.LoadAsync(SceneDefine.InGame).ToObservable())
                .Subscribe()
                .AddTo(_disposable);

            _menuView.OnClickLicenseButtonAsObservable()
                .Subscribe(_ => _licensePresenter.Show())
                .AddTo(_disposable);
        }

        public async UniTask Show()
        {
            await _menuView.Show();
        }

        public async UniTask Hide()
        {
            await _menuView.Hide();
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
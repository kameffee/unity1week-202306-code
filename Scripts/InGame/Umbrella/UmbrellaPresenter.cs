using System;
using UniRx;
using VContainer.Unity;

namespace Unity1week202306.InGame.Umbrella
{
    public class UmbrellaPresenter : IInitializable, ITickable, IDisposable
    {
        private readonly UmbrellaCursorView _umbrellaCursorView;
        private readonly UmbrellaController _umbrellaController;

        private readonly CompositeDisposable _disposable = new();
        
        public UmbrellaPresenter(
            UmbrellaCursorView umbrellaCursorView,
            UmbrellaController umbrellaController)
        {
            _umbrellaCursorView = umbrellaCursorView;
            _umbrellaController = umbrellaController;
        }

        void IInitializable.Initialize()
        {
            _umbrellaController.IsOpened
                .Subscribe(isOpened => _umbrellaCursorView.SetOpened(isOpened))
                .AddTo(_disposable);
        }

        void ITickable.Tick()
        {
            if (_umbrellaController.IsActivate)
            {
                _umbrellaController.SetDirection(_umbrellaCursorView.Direction);
            }
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
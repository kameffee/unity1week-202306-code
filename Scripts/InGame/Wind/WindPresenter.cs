using System;
using UniRx;
using VContainer.Unity;

namespace Unity1week202306.InGame.Wind
{
    public class WindPresenter : IInitializable, IDisposable
    {
        private readonly WindSituation _windSituation;
        private readonly WindView _windView;
        private readonly CompositeDisposable _disposable = new();

        public WindPresenter(WindSituation windSituation, WindView windView)
        {
            _windSituation = windSituation;
            _windView = windView;
        }

        public void Initialize()
        {
            _windSituation.WindSpeed
                .Subscribe(windSpeed => _windView.SetDirection(windSpeed.Value))
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
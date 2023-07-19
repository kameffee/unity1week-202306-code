using UniRx;
using VContainer.Unity;

namespace Unity1week202306.InGame.DeadAreas
{
    public class DeadAreaPresenter : IInitializable
    {
        private readonly DeadArea _deadArea;
        private readonly RespawnUseCase _respawnUseCase;
        private readonly CompositeDisposable _disposable = new();

        public DeadAreaPresenter(DeadArea deadArea, RespawnUseCase respawnUseCase)
        {
            _deadArea = deadArea;
            _respawnUseCase = respawnUseCase;
        }

        public void Initialize()
        {
            _deadArea.OnEnterDeadAreaAsObservable()
                .Subscribe(playerController => _respawnUseCase.Respawn())
                .AddTo(_disposable);
        }
    }
}
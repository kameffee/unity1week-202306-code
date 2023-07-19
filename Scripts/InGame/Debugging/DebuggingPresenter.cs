using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202306.InGame.Debugging
{
    public class DebuggingPresenter : IInitializable, IDisposable
    {
        [Inject]
        private readonly DebuggingUseCase _debuggingUseCase;

        private readonly CompositeDisposable _disposable = new();

        void IInitializable.Initialize()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Alpha1))
                .Subscribe(_ => _debuggingUseCase.TeleportToNextCheckPoint())
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
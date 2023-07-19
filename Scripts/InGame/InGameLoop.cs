using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity1week202306.Audio;
using Unity1week202306.InGame.CheckPoints;
using Unity1week202306.InGame.Finish;
using Unity1week202306.InGame.Goal;
using Unity1week202306.InGame.Players;
using Unity1week202306.InGame.Umbrella;
using Unity1week202306.InGame.Wind;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202306.InGame
{
    public class InGameLoop : IAsyncStartable, IInitializable, IDisposable
    {
        private readonly ChangeWindUseCase _changeWindUseCase;
        private readonly FieldCheckPointRepository _fieldCheckPointRepository;
        private readonly GoalUseCase _goalUseCase;
        private readonly FinishPerformUseCase _finishPerformUseCase;
        private readonly PlayerController _playerController;
        private readonly UmbrellaCursorService _umbrellaCursorService;
        private readonly AudioPlayer _audioPlayer;

        private readonly CompositeDisposable _disposable = new();

        public InGameLoop(
            ChangeWindUseCase changeWindUseCase,
            FieldCheckPointRepository fieldCheckPointRepository,
            GoalUseCase goalUseCase,
            FinishPerformUseCase finishPerformUseCase,
            PlayerController playerController,
            UmbrellaCursorService umbrellaCursorService,
            AudioPlayer audioPlayer)
        {
            _changeWindUseCase = changeWindUseCase;
            _fieldCheckPointRepository = fieldCheckPointRepository;
            _goalUseCase = goalUseCase;
            _finishPerformUseCase = finishPerformUseCase;
            _playerController = playerController;
            _umbrellaCursorService = umbrellaCursorService;
            _audioPlayer = audioPlayer;
        }


        public void Initialize()
        {
            _goalUseCase.OnGoalAsObservable()
                .Subscribe(_ => FinishAsync(CancellationToken.None).Forget())
                .AddTo(_disposable);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _audioPlayer.PlayBgm("InGameBgm");

            await Prepare();

            while (!cancellation.IsCancellationRequested)
            {
                _changeWindUseCase.ChangeRandom();
                await UniTask.Delay(TimeSpan.FromSeconds(10), cancellationToken: cancellation);
            }
        }

        private async UniTask Prepare()
        {
            // チェックポイントオブジェクトをシーン上からかき集める
            var checkPointObjects = GameObject.FindObjectsByType<CheckPointObject>(FindObjectsSortMode.None);
            _fieldCheckPointRepository.Set(checkPointObjects);

            _playerController.Activate(true);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            await _umbrellaCursorService.ShowAsync(1f);
        }

        /// <summary>
        /// ゲーム終了時処理
        /// </summary>
        /// <param name="cancellationToken"></param>
        private async UniTask FinishAsync(CancellationToken cancellationToken)
        {
            // 入力を無効にする
            _playerController.Activate(false);
            _playerController.UmbrellaController.Activate(false);

            _umbrellaCursorService.HideAsync(2).Forget();

            // ゲーム終了演出
            await _finishPerformUseCase.PerformAsync(cancellationToken);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
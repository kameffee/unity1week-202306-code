using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity1week202306.InGame.Players;
using Unity1week202306.LetterBox;
using Unity1week202306.Scenes;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202306.InGame.Finish
{
    public class FinishSequencePresenter : IInitializable
    {
        private readonly FinishPerformer _finishPerformer;
        private readonly PlayerController _playerController;
        private readonly LetterBoxUseService _letterBoxUseService;
        private readonly SceneLoader _sceneLoader;

        private readonly CompositeDisposable _disposable = new();

        public FinishSequencePresenter(
            FinishPerformer finishPerformer,
            PlayerController playerController,
            LetterBoxUseService letterBoxUseService,
            SceneLoader sceneLoader)
        {
            _finishPerformer = finishPerformer;
            _playerController = playerController;
            _letterBoxUseService = letterBoxUseService;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _finishPerformer.OnClickTitleButtonAsObservable()
                .Subscribe(_ => _sceneLoader.LoadAsync(SceneDefine.Title).Forget())
                .AddTo(_disposable);
        }

        public async UniTask Play(CancellationToken cancellationToken)
        {
            // 落下しないようにする
            _playerController.ActiveGravity(false);
            // 傘を開く
            _playerController.UmbrellaController.Open();
            // 傘の向きを上方向固定にする
            _playerController.UmbrellaController.SetDirection(Vector2.up);
            _playerController.UmbrellaController.Activate(false);

            // クリア演出
            _finishPerformer.Play(_playerController, cancellationToken).Forget();

            await UniTask.WhenAll(
                _letterBoxUseService.ShowAsync(cancellationToken: cancellationToken),
                UniTask.Delay(TimeSpan.FromSeconds(8), cancellationToken: cancellationToken)
            );

            await _finishPerformer.ShowThanksMessage(cancellationToken);

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

            _finishPerformer.ShowTitleButton();
        }
    }
}
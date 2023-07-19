using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity1week202306.Audio;
using Unity1week202306.Config;
using Unity1week202306.InGame.Players;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202306.Title
{
    public class TitleEntryPoint : IAsyncStartable
    {
        private readonly TitleTextView _titleTextView;
        private readonly ConfigPresenter _configPresenter;
        private readonly MenuPresenter _menuPresenter;
        private readonly PlayerController _playerController;
        private readonly AudioPlayer _audioPlayer;

        public TitleEntryPoint(
            TitleTextView titleTextView,
            ConfigPresenter configPresenter,
            MenuPresenter menuPresenter,
            PlayerController playerController,
            AudioPlayer audioPlayer)
        {
            _titleTextView = titleTextView;
            _configPresenter = configPresenter;
            _menuPresenter = menuPresenter;
            _playerController = playerController;
            _audioPlayer = audioPlayer;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _audioPlayer.PlayBgm("TitleBgm");
            _playerController.UmbrellaController.SetDirection(125f);

            await UniTask.WhenAny(
                UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: cancellation),
                UniTask.WaitUntil(() => Input.anyKey, cancellationToken: cancellation),
                UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: cancellation)
            );

            await _titleTextView.Show();

            await UniTask.WhenAny(
                UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: cancellation),
                UniTask.WaitUntil(() => Input.anyKey, cancellationToken: cancellation),
                UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: cancellation)
            );

            _playerController.UmbrellaController.Open();

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellation);

            await _menuPresenter.Show();

            await _configPresenter.Show();
        }
    }
}
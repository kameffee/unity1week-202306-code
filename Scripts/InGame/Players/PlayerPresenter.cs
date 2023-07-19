using System;
using UniRx;
using Unity1week202306.InGame.CheckPoints;
using Unity1week202306.InGame.Inputs;
using Unity1week202306.InGame.Umbrella;
using Unity1week202306.InGame.Wind;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202306.InGame.Players
{
    public class PlayerPresenter : IInitializable, ITickable, IDisposable
    {
        private readonly PlayerController _playerController;
        private readonly UmbrellaController _umbrellaController;
        private readonly WindSituation _windSituation;
        private readonly SetCheckPointUseCase _setCheckPointUseCase;

        private readonly CompositeDisposable _disposable = new();

        public PlayerPresenter(
            PlayerController playerController,
            UmbrellaController umbrellaController,
            WindSituation windSituation,
            SetCheckPointUseCase setCheckPointUseCase)
        {
            _playerController = playerController;
            _umbrellaController = umbrellaController;
            _windSituation = windSituation;
            _setCheckPointUseCase = setCheckPointUseCase;
        }

        public void Initialize()
        {
            _windSituation.WindSpeed
                .Subscribe(windSpeed => _playerController.SetWindResistance(windSpeed))
                .AddTo(_disposable);

            _playerController.OnCheckPointerAsObservable()
                .Subscribe(checkPoint => _setCheckPointUseCase.Execute(checkPoint.ToStartPoint().Id))
                .AddTo(_disposable);
        }

        public void Tick()
        {
            if (_umbrellaController.IsActivate && Input.GetMouseButtonDown(0))
            {
                _umbrellaController.Switch();
            }

            var inputData = new InputData()
            {
                Horizontal = Input.GetAxisRaw("Horizontal"),
                Vertical = Input.GetAxisRaw("Vertical"),
                IsJump = Input.GetButtonDown("Jump"),
            };

            _playerController.SetInputData(inputData);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
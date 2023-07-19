using System;
using UniRx;
using VContainer.Unity;

namespace Unity1week202306.InGame.Goal
{
    public class GoalPresenter : IInitializable, IDisposable
    {
        private readonly GoalArea _goalArea;
        private readonly GoalUseCase _goalUseCase;
        private readonly CompositeDisposable _disposable = new();

        public GoalPresenter(
            GoalArea goalArea,
            GoalUseCase goalUseCase)
        {
            _goalArea = goalArea;
            _goalUseCase = goalUseCase;
        }

        public void Initialize()
        {
            _goalArea.OnGoalAsObservable()
                .Subscribe(_ => _goalUseCase.Goal())
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
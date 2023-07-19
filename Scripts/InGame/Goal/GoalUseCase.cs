using System;
using UniRx;

namespace Unity1week202306.InGame.Goal
{
    public class GoalUseCase
    {
        private readonly ISubject<Unit> _onGoal = new Subject<Unit>();
        
        public IObservable<Unit> OnGoalAsObservable() => _onGoal;
        
        public void Goal() => _onGoal.OnNext(Unit.Default);
    }
}
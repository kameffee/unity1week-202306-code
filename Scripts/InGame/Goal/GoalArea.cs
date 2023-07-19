using System;
using UniRx;
using Unity1week202306.InGame.Players;
using UnityEngine;

namespace Unity1week202306.InGame.Goal
{
    public class GoalArea : MonoBehaviour
    {
        private readonly Subject<PlayerController> _onGoal = new();

        public IObservable<PlayerController> OnGoalAsObservable() => _onGoal;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // プレイヤー判定
            if (other.TryGetComponent<PlayerController>(out var playerController))
            {
                _onGoal.OnNext(playerController);
            }
        }
    }
}
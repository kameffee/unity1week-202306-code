using System;
using UniRx;
using Unity1week202306.InGame.Players;
using UnityEngine;

namespace Unity1week202306.InGame.DeadAreas
{
    public class DeadArea : MonoBehaviour
    {
        private readonly Subject<PlayerController> _onEnterDeadAreaAsObservable = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerController>(out var playerController))
            {
                _onEnterDeadAreaAsObservable.OnNext(playerController);
            }
        }

        public IObservable<PlayerController> OnEnterDeadAreaAsObservable() => _onEnterDeadAreaAsObservable;

        private void OnDestroy()
        {
            _onEnterDeadAreaAsObservable?.Dispose();
        }
    }
}
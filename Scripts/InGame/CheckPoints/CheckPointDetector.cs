using System;
using UniRx;
using UnityEngine;

namespace Unity1week202306.InGame.CheckPoints
{
    public class CheckPointDetector : MonoBehaviour
    {
        public IObservable<CheckPointObject> OnCheckPointEnter => _onCheckPointEnter;
        private readonly Subject<CheckPointObject> _onCheckPointEnter = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<CheckPointObject>(out var checkPointObject))
            {
                _onCheckPointEnter.OnNext(checkPointObject);
            }
        }
    }
}
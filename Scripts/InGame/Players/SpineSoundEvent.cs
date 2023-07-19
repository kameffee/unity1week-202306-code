using System;
using UnityEngine;

namespace Unity1week202306.InGame.Players
{
    [Serializable]
    public class SpineSoundEvent
    {
        [SerializeField]
        private string _eventName;

        [SerializeField]
        private AudioClip _audioClip;

        public string EventName => _eventName;
        public AudioClip AudioClip => _audioClip;
    }
}
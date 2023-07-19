using System.Collections.Generic;
using Spine;
using Spine.Unity;
using Unity1week202306.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Event = Spine.Event;

namespace Unity1week202306.InGame.Players
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField]
        private GameObject _root;

        [SerializeField]
        private SkeletonAnimation _skeletonAnimation;

        [Header("Sound")]
        [SerializeField]
        private List<SpineSoundEvent> _soundEventList = new();

        [Header("Body Animations")]
        [SerializeField]
        [SpineAnimation]
        private string _walkAnimationName;

        [SerializeField]
        [SpineAnimation]
        private string[] _idleAnimationNames;

        [SerializeField]
        [SpineAnimation]
        private string _jumpingAnimationName;

        [SerializeField]
        [SpineAnimation]
        private string _fallingAnimationName;

        [SerializeField]
        [SpineAnimation]
        private string _landingAnimationName;

        [Header("Head Animations")]
        [SerializeField]
        [SpineAnimation]
        private string _headAnimationName;

        [Header("Eyes Animations")]
        [SerializeField]
        [SpineAnimation]
        private string _eyeBlinkAnimationName;

        private const int kBodySlotIndex = 0;
        private const int kHeadSlotIndex = 1;
        private const int kEyesSlotIndex = 2;

        [Inject]
        private readonly AudioPlayer _audioPlayer;

        private void Start()
        {
            _skeletonAnimation.AnimationState.SetAnimation(kEyesSlotIndex, _eyeBlinkAnimationName, true);
            _skeletonAnimation.AnimationState.SetAnimation(kHeadSlotIndex, _headAnimationName, true);

            var lifetimeScope = LifetimeScope.Find<LifetimeScope>(gameObject.scene);
            lifetimeScope.Container.Inject(this);
        }

        public void SetDirection(Vector2 direction)
        {
            _root.transform.localScale = (direction.x < 0) ? new Vector3(-1f, 1f, 1f) : Vector3.one;
        }

        public void Idle()
        {
            var nextAnimationName = _idleAnimationNames[Random.Range(0, _idleAnimationNames.Length)];
            var current = _skeletonAnimation.AnimationState.GetCurrent(kBodySlotIndex);
            if (current.Animation.Name != nextAnimationName)
            {
                _skeletonAnimation.AnimationState.SetAnimation(kBodySlotIndex, nextAnimationName, true);
            }
        }

        public void SetWalk(Vector2 direction)
        {
            var nextAnimationName = direction.x != 0
                ? _walkAnimationName
                : _idleAnimationNames[Random.Range(0, _idleAnimationNames.Length)];
            var current = _skeletonAnimation.AnimationState.GetCurrent(kBodySlotIndex);
            if (current.Animation.Name != nextAnimationName)
            {
                var trackEntry =
                    _skeletonAnimation.AnimationState.SetAnimation(kBodySlotIndex, nextAnimationName, true);
                trackEntry.Event += TrackEntryOnEvent;
            }
        }

        private void TrackEntryOnEvent(TrackEntry trackentry, Event e)
        {
            foreach (var spineSoundEvent in _soundEventList)
            {
                if (e.Data.Name.Equals(spineSoundEvent.EventName))
                {
                    _audioPlayer.PlaySe(spineSoundEvent.AudioClip);
                }
            }
        }

        public void OnJumping()
        {
            if (_skeletonAnimation.AnimationState.GetCurrent(kBodySlotIndex).Animation.Name != _fallingAnimationName)
            {
                var trackEntry =
                    _skeletonAnimation.AnimationState.SetAnimation(kBodySlotIndex, _jumpingAnimationName, false);
                _skeletonAnimation.AnimationState.AddAnimation(kBodySlotIndex, _fallingAnimationName, false, 0.1f);

                trackEntry.Event += TrackEntryOnEvent;
            }
        }

        public void OnFalling()
        {
            if (_skeletonAnimation.AnimationState.GetCurrent(kBodySlotIndex).Animation.Name != _fallingAnimationName)
            {
                var trackEntry =
                    _skeletonAnimation.AnimationState.SetAnimation(kBodySlotIndex, _fallingAnimationName, false);
                trackEntry.Event += TrackEntryOnEvent;
            }
        }

        public void OnGrounded()
        {
            var nextAnimationName = _landingAnimationName;
            var current = _skeletonAnimation.AnimationState.GetCurrent(kBodySlotIndex);
            if (current.Animation.Name != nextAnimationName)
            {
                var trackEntry =
                    _skeletonAnimation.AnimationState.SetAnimation(kBodySlotIndex, nextAnimationName, true);
                trackEntry.MixDuration = 0.1f;
                trackEntry.Event += TrackEntryOnEvent;
            }
        }
    }
}
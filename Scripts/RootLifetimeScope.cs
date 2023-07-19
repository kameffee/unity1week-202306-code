using Unity1week202306.Audio;
using Unity1week202306.Scenes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202306
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private SceneTransitionView _transitionViewPrefab;

        [SerializeField]
        private AudioResource _audioResource;

        protected override void Configure(IContainerBuilder builder)
        {
            BuildSceneLoad(builder);
            BuildAudio(builder);
        }

        private void BuildSceneLoad(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab<SceneTransitionView>(_transitionViewPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad();
        }

        private void BuildAudio(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab<AudioResource>(_audioResource, Lifetime.Singleton).DontDestroyOnLoad();

            builder.Register<AudioResourceLoader>(Lifetime.Singleton);
            builder.Register<AudioPlayer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.Register<AudioSettingsService>(Lifetime.Singleton);

            builder.RegisterComponentOnNewGameObject<BgmPlayer>(Lifetime.Singleton).DontDestroyOnLoad();
            builder.RegisterComponentOnNewGameObject<SePlayer>(Lifetime.Singleton).DontDestroyOnLoad();
        }
    }
}
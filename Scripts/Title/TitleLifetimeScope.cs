using Unity1week202306.Config;
using Unity1week202306.InGame.Players;
using Unity1week202306.License;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202306.Title
{
    public class TitleLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private LicenseView _licenseViewPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PlayerController>();
            builder.RegisterComponentInHierarchy<TitleTextView>();

            BuildMenu(builder);
            BuildAudioConfig(builder);
            BuildLicense(builder);

            builder.RegisterEntryPoint<TitleEntryPoint>();
        }

        private void BuildMenu(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<MenuView>();
            builder.Register<MenuPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        }

        private void BuildAudioConfig(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<AudioConfigView>();
            builder.Register<ConfigPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        }

        private void BuildLicense(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab<LicenseView>(_licenseViewPrefab, Lifetime.Scoped);
            builder.Register<GetLicenseTextUseCase>(Lifetime.Scoped);
            builder.Register<LicensePresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        }
    }
}
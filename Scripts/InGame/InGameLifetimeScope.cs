using System.Diagnostics;
using Unity1week202306.InGame.CheckPoints;
using Unity1week202306.InGame.DeadAreas;
using Unity1week202306.InGame.Debugging;
using Unity1week202306.InGame.Finish;
using Unity1week202306.InGame.Goal;
using Unity1week202306.InGame.Players;
using Unity1week202306.InGame.Umbrella;
using Unity1week202306.InGame.Wind;
using Unity1week202306.LetterBox;
using VContainer;
using VContainer.Unity;

namespace Unity1week202306.InGame
{
    public class InGameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            PlayerBuild(builder);
            UmbrellaBuild(builder);
            BuildWind(builder);
            BuildCheckPoint(builder);
            BuildDeadArea(builder);
            BuildGoal(builder);
            BuildFinish(builder);
            BuildDebugging(builder);
            BuildLetterBox(builder);

            builder.RegisterEntryPoint<InGameLoop>();
        }

        private void PlayerBuild(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PlayerController>();
            builder.RegisterEntryPoint<PlayerPresenter>();
        }

        private void UmbrellaBuild(IContainerBuilder builder)
        {
            // Cursor
            builder.RegisterComponentInHierarchy<UmbrellaCursorView>();
            builder.Register<UmbrellaCursorService>(Lifetime.Scoped);

            builder.RegisterComponentInHierarchy<UmbrellaController>();
            builder.RegisterEntryPoint<UmbrellaPresenter>();
        }

        private void BuildWind(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<WindView>();
            builder.Register<WindService>(Lifetime.Scoped);
            builder.Register<WindSituation>(Lifetime.Scoped);
            builder.Register<ChangeWindUseCase>(Lifetime.Scoped);
            builder.RegisterEntryPoint<WindPresenter>();
        }

        private void BuildCheckPoint(IContainerBuilder builder)
        {
            builder.Register<CheckPointService>(Lifetime.Scoped);
            builder.Register<SetCheckPointUseCase>(Lifetime.Scoped);
            builder.Register<FieldCheckPointRepository>(Lifetime.Scoped);
            builder.Register<RespawnUseCase>(Lifetime.Scoped);
        }

        private void BuildDeadArea(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<DeadArea>();
            builder.RegisterEntryPoint<DeadAreaPresenter>();
        }

        private void BuildGoal(IContainerBuilder builder)
        {
            builder.Register<GoalUseCase>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<GoalArea>();
            builder.RegisterEntryPoint<GoalPresenter>();
        }

        private void BuildFinish(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<FinishPerformer>();
            builder.Register<FinishPerformUseCase>(Lifetime.Scoped);
            builder.Register<FinishSequencePresenter>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        }

        [Conditional("DEBUG")]
        private void BuildDebugging(IContainerBuilder builder)
        {
            builder.Register<DebuggingUseCase>(Lifetime.Scoped);
            builder.RegisterEntryPoint<DebuggingPresenter>();
        }

        private void BuildLetterBox(IContainerBuilder builder)
        {
            builder.Register<LetterBoxUseService>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<LetterBoxView>();

            builder.RegisterBuildCallback(resolver => { resolver.Resolve<LetterBoxUseService>(); });
        }
    }
}
using Application.StateMachine;
using Application.StateMachine.States;
using Application.UI;
using Application.UI.Common;
using Common.StateMachine;
using Zenject;

namespace Application.Installer
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallStateMachine();
            InstallSignals();
            InstallLoader();
            InstallUI();
        }

        private void InstallUI()
        {
            Container.BindInterfacesAndSelfTo<ScreenController>().AsSingle().NonLazy();
        }

        private void InstallLoader()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader.SceneLoader>().AsSingle();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ApplicationStateMachine.Signals.OnState>().OptionalSubscriber();
        }
        private void InstallStateMachine()
        {
            Container.Bind<IInitializable>().To<ApplicationStateMachine>().AsSingle();

            Container.BindFactory<IState, LoadingState.Factory>()
                .To<LoadingState>()
                .WhenInjectedInto<ApplicationStateMachine>();
        
            Container.BindFactory<IState, GameplayState.Factory>()
                .To<GameplayState>()
                .WhenInjectedInto<ApplicationStateMachine>();
        }
    }
}
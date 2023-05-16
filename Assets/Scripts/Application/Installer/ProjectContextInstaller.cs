using Application.StateMachine;
using Application.StateMachine.States;
using Application.UI;
using Application.UI.Common;
using Common.StateMachine;
using DG.Tweening;
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
            InitialiseDOTween();
        }

        private void InitialiseDOTween()
        {
            DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(500, 50);
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
            Container.Bind(typeof(ApplicationStateMachine)).ToSelf().AsSingle().NonLazy();

            Container.BindFactory<IState, LoadingState.Factory>()
                .To<LoadingState>()
                .WhenInjectedInto<ApplicationStateMachine>();
        
            Container.BindFactory<IState, GameplayState.Factory>()
                .To<GameplayState>()
                .WhenInjectedInto<ApplicationStateMachine>();
        }
    }
}
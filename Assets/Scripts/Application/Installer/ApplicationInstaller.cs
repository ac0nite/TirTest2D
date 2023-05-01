using Application;
using Application.StateMachine;
using Application.StateMachine.States;
using Common.ApplicationStateMachine;
using Common.SceneLoader;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInitializable>().To<ApplicationStateMachine>().AsSingle();

        Container.BindFactory<IState, LoadingState.Factory>()
            .To<LoadingState>()
            .WhenInjectedInto<ApplicationStateMachine>();
        
        Container.BindFactory<IState, GameplayState.Factory>()
            .To<GameplayState>()
            .WhenInjectedInto<ApplicationStateMachine>();

        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
     
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<ApplicationStateMachine.Signals.OnState>().OptionalSubscriber();
    }
}
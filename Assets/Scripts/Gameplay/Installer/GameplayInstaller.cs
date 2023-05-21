using System;
using Common.StateMachine;
using Gameplay.Bullets;
using Gameplay.Enemy;
using Gameplay.Input;
using Gameplay.Player;
using Gameplay.Settings;
using Gameplay.StateMachine;
using Gameplay.StateMachine.States;
using Gameplay.UI;
using Resources;
using Zenject;

namespace Gameplay.Installer
{
    public class GameplayInstaller : MonoInstaller
    {
        private readonly GameplayResources _resources;

        public GameplayInstaller(GameplayResources resources)
        {
            _resources = resources;
        }
        
        public override void InstallBindings()
        {
            InstallStateMachine();
            InstallInput();
            InstallSignals();

            Container.BindInterfacesAndSelfTo<GameplayBackground>().AsSingle();

            BulletsInstaller.Install(Container);
            PlayerInstaller.Install(Container);
            GameplayUIInstaller.Install(Container);
            EnemyInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<GameplayLevelManager>().AsSingle();
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<GameplayStateMachine.Signals.NextState>().OptionalSubscriber();
        }

        private void InstallStateMachine()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<GameplayStateMachine>().AsSingle();
            
            Container.BindFactory<IState, PreGameplayState.Factory>()
                .To<PreGameplayState>()
                .WhenInjectedInto<GameplayStateMachine>();
            
            Container.BindFactory<IState, LoadingGameplayState.Factory>()
                .To<LoadingGameplayState>()
                .WhenInjectedInto<GameplayStateMachine>();
            
            Container.BindFactory<IState, PreviewGameplayState.Factory>()
                .To<PreviewGameplayState>()
                .WhenInjectedInto<GameplayStateMachine>();
            
            Container.BindFactory<IState, RunPlayGameplayState.Factory>()
                .To<RunPlayGameplayState>()
                .WhenInjectedInto<GameplayStateMachine>();
            
            Container.BindFactory<IState, ResultGameplayState.Factory>()
                .To<ResultGameplayState>()
                .WhenInjectedInto<GameplayStateMachine>();
        }
        private void InstallInput()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
    }
}
using System;
using Common.StateMachine;
using Gameplay.Bullets;
using Gameplay.StateMachine;
using Gameplay.StateMachine.States;
using Resources;
using Zenject;

namespace Gameplay.Installer
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallStateMachine();
            InstallInput();
            InstallWeapon();
            InstallSignals();
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
            
            Container.BindFactory<IState, GameGameplayState.Factory>()
                .To<GameGameplayState>()
                .WhenInjectedInto<GameplayStateMachine>();
            
            Container.BindFactory<IState, ResultGameplayState.Factory>()
                .To<ResultGameplayState>()
                .WhenInjectedInto<GameplayStateMachine>();
        }
        private void InstallInput()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
        private void InstallWeapon()
        {
            Container.BindMemoryPool<Bomb, Bomb.Pool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefabResource(Constants.Resources.Bomb)
                .UnderTransformGroup("Bomb_pool")
                .NonLazy();
            
            Container.BindMemoryPool<Cannonball, Cannonball.Pool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefabResource(Constants.Resources.Cannonball)
                .UnderTransformGroup("Cannonball_pool")
                .NonLazy();

            Container.Bind(typeof(BombSpawner)).ToSelf().AsSingle();
            Container.Bind(typeof(CannonballSpawner)).ToSelf().AsSingle();

            Container.BindInterfacesAndSelfTo<WeaponModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletFacade>().AsSingle();
        }
    }
}
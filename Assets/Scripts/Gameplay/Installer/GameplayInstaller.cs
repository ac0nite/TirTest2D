using ApplicationConstant;
using Gameplay.Bullets;
using Zenject;

namespace Gameplay.Installer
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();

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
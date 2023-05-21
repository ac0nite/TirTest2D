using System.Linq;
using Gameplay.Bullets.Settings;
using Gameplay.Models;
using Gameplay.Settings;
using Zenject;

namespace Gameplay.Bullets
{
    public class BulletsInstaller : Installer<BulletsInstaller>
    {
        private readonly GameplayResources _resources;

        public BulletsInstaller(GameplayResources resources)
        {
            _resources = resources;
        }
        public override void InstallBindings()
        {
            var bombPrefab = _resources.BulletResources.First(i => i.Type == BulletType.BOMB).BulletPrefab;
            var cannonballPrefab = _resources.BulletResources.First(i => i.Type == BulletType.CANNONBALL).BulletPrefab;

            Container.BindMemoryPool<Bomb, Bomb.Pool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(bombPrefab)
                .UnderTransformGroup("BOMB_POOL");

            Container.BindMemoryPool<Cannonball, Cannonball.Pool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(cannonballPrefab)
                .UnderTransformGroup("CANNONBALL_POOL");

            Container.Bind(typeof(BombSpawner)).ToSelf().AsSingle();
            Container.Bind(typeof(CannonballSpawner)).ToSelf().AsSingle();

            Container.BindInterfacesAndSelfTo<GameplayModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletFacade>().AsSingle();
        }
    }
}
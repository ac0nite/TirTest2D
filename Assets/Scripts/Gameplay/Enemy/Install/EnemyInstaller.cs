using Gameplay.Bullets;
using Gameplay.Enemy.BirdItem;
using Gameplay.Settings;
using Zenject;

namespace Gameplay.Enemy
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        private readonly GameplayResources _resources;

        public EnemyInstaller(GameplayResources resources)
        {
            _resources = resources;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GeneratorRandomSpawnPoint>().AsSingle();
            
            Container.BindMemoryPool<Bird, Bird.Pool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_resources.BirdPrefab)
                .UnderTransformGroup("BIRDS_POOL");

            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnGenerator>().AsSingle();
        }
    }
}
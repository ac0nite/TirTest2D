using Gameplay.Settings;
using Zenject;
namespace Gameplay.Player
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        private readonly GameplayResources _resources;

        public PlayerInstaller(GameplayResources resources)
        {
            _resources = resources;
        }
        public override void InstallBindings()
        {
            Container.BindFactory<Cannon, Cannon.Factory>()
                .FromComponentInNewPrefab(_resources.CannonPrefab)
                .WhenInjectedInto<CannonKeeper>();

            Container.BindInterfacesAndSelfTo<CannonKeeper>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShooting>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerState>().AsSingle();
            Container.BindInterfacesAndSelfTo<CannonHelper>().AsSingle();
        }
    }
}
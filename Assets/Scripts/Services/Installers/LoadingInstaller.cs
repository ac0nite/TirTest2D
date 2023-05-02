using Zenject;

namespace Services.Installers
{
    public class LoadingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallGameLoader();
        }

        private void InstallGameLoader()
        {
            Container.Bind(typeof(IInitializable)).To<GameLoader>().AsSingle();
        }
    }
}
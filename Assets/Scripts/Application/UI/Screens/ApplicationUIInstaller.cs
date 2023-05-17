using Resources;
using UnityEngine;
using Zenject;

namespace Application.UI.Screens
{
    public class ApplicationUIInstaller : Installer<ApplicationUIInstaller>
    {
        private readonly Canvas _applicationCanvas;

        public ApplicationUIInstaller(
            [Inject (Id = Constants.ID.ApplicationCanvas)] Canvas canvas)
        {
            _applicationCanvas = canvas;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>()
                .FromComponentInNewPrefabResource(Constants.Resources.SplashScreen)
                .UnderTransform(_applicationCanvas.transform)
                .AsTransient()
                .WhenInjectedInto()
                .NonLazy();

            Container.Bind<Transform>()
                .FromComponentInNewPrefabResource(Constants.Resources.LoadingScreen)
                .UnderTransform(_applicationCanvas.transform)
                .AsTransient()
                .WhenInjectedInto()
                .NonLazy();
        }
    }
}
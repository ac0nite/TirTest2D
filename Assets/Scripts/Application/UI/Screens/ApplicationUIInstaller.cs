using Application.UI.Common;
using UnityEngine;
using Zenject;

namespace Application.UI.Screens
{
    public class ApplicationUIInstaller : Installer<ApplicationUIInstaller>
    {
        private readonly Canvas _applicationCanvas;

        public ApplicationUIInstaller(
            [Inject (Id = ApplicationConstants.ID.ApplicationCanvas)] Canvas canvas)
        {
            _applicationCanvas = canvas;
        }
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScreenController>().AsSingle().NonLazy();

            Container.Bind<IGameplayScreen>()
                .FromComponentInNewPrefabResource(ApplicationConstants.Resources.ApplicationScreen)
                .UnderTransform(_applicationCanvas.transform)
                .AsSingle()
                .NonLazy();
        }
    }
}
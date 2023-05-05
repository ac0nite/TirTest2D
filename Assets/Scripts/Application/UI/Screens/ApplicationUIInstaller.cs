using Application.UI.Common;
using ApplicationConstant;
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
            Container.Bind<IGameplayScreen>()
                .FromComponentInNewPrefabResource(Constants.Resources.ApplicationScreen)
                .UnderTransform(_applicationCanvas.transform)
                .AsSingle()
                .NonLazy();
        }
    }
}
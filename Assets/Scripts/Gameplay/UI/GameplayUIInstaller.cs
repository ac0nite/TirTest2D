using Resources;
using UnityEngine;
using Zenject;

namespace Gameplay.UI
{
    public class GameplayUIInstaller : Installer<GameplayUIInstaller>
    {
        private readonly RectTransform _transform;

        public GameplayUIInstaller([Inject (Id = Constants.ID.GameplayCanvas)] RectTransform rectTransform)
        {
            _transform = rectTransform;
        }
        public override void InstallBindings()
        {
            Container.Bind<Transform>()
                .FromComponentInNewPrefabResource(Constants.Resources.PreviewScreen)
                .UnderTransform(_transform)
                .AsTransient()
                .WhenInjectedInto()
                .NonLazy();
            
            Container.Bind<Transform>()
                .FromComponentInNewPrefabResource(Constants.Resources.GameplayScreen)
                .UnderTransform(_transform)
                .AsTransient()
                .WhenInjectedInto()
                .NonLazy();
            
            Container.Bind<Transform>()
                .FromComponentInNewPrefabResource(Constants.Resources.ResultScreen)
                .UnderTransform(_transform)
                .AsTransient()
                .WhenInjectedInto()
                .NonLazy();
        }
    }
}
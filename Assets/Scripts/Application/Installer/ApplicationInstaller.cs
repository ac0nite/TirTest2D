using Application.UI.Screens;
using Zenject;

namespace Application.Installer
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ApplicationUIInstaller.Install(Container);
        }
    }
}
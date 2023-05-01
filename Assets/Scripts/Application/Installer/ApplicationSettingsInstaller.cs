using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ApplicationSettingsInstaller", menuName = "Installers/ApplicationSettingsInstaller")]
public class ApplicationSettingsInstaller : ScriptableObjectInstaller<ApplicationSettingsInstaller>
{
    public override void InstallBindings()
    {
    }
}
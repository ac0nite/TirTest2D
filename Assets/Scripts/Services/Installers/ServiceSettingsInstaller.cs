using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ServiceSettingsInstaller", menuName = "Installers/ServiceSettingsInstaller")]
public class ServiceSettingsInstaller : ScriptableObjectInstaller<ServiceSettingsInstaller>
{
    public override void InstallBindings()
    {
    }
}
using DI.Contract;
using TowerDefence.Managers;

namespace TowerDefence.Core.Installers
{
    public sealed class ManagersInstallers
    {
        public void InstallTo(IDiContainer diContainer)
        {
            diContainer.BindSingleton<MenuManager>();
            diContainer.BindSingleton<GameManager>();
        }
    }
}
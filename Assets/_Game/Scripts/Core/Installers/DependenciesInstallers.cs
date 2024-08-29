using DI.Contract;
using TowerDefence.UI.Shared.Presenter;
using TowerDefence.UI.Shared.View;

namespace TowerDefence.Core.Installers
{
    public sealed class DependenciesInstallers
    {
        public void InstallTo(IDiContainer diContainer)
        {
            diContainer.BindSingleton<ViewDependencies>();
            diContainer.BindSingleton<PresenterDependencies>();
        }
    }
}
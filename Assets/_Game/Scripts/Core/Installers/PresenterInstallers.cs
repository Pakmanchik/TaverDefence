using DI.Contract;
using TowerDefence.UI.Menu.Presenters;

namespace TowerDefence.Core.Installers
{
    public sealed class PresenterInstallers
    {
        public void InstallTo(IDiContainer diContainer)
        {
            diContainer.BindSingleton<MenuPresenter>();
        }
    }
}
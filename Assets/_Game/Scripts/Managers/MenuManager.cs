using Systems.LoadingScreen.Contract;
using TowerDefence.UI.Menu.Presenters;

namespace TowerDefence.Managers
{
    public sealed class MenuManager : IManager
    {
        private readonly ILoadScreenSystem _loadScreenSystem;
        private readonly MenuPresenter _menuPresenter;

        public MenuManager(ILoadScreenSystem loadScreenSystem, MenuPresenter menuPresenter)
        {
            _loadScreenSystem = loadScreenSystem;
            _menuPresenter    = menuPresenter;
        }

        public async void Show()
        {
            _loadScreenSystem.SetPercent(10);
            
            await _menuPresenter.ShowWindow();
            
            _loadScreenSystem.SetPercent(100);
        }

        public void Close()
        {
            _menuPresenter.HideWindow();
        }
    }
}
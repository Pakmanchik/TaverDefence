using Systems.LoadingScreen.Contract;

namespace TowerDefence.Managers
{
    public sealed class GameManager : IManager
    {
        private readonly ILoadScreenSystem _loadScreenSystem;

        public GameManager(ILoadScreenSystem loadScreenSystem)
        {
            _loadScreenSystem = loadScreenSystem;
        }
        
        public void Show()
        {
            _loadScreenSystem.Show();
            _loadScreenSystem.SetPercent(10);
            
            // что-то
            
            _loadScreenSystem.SetPercent(100);
            _loadScreenSystem.Hide();
        }

        public void Close()
        {
        }
    }
}
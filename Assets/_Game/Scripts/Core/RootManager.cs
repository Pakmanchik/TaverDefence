using DI.Contract;
using MessageBus.Contract;
using Systems.LoadingScreen.Contract;
using TowerDefence.Managers;
using TowerDefence.UI.Messages;
using Zenject;

namespace TowerDefence.Core
{
    public sealed class RootManager
    {
        [Inject] private readonly GameManager _gameManager;
        [Inject] private readonly MenuManager _menuManager;

        [Inject] private readonly IMessageBus _messageBus;
        [Inject] private readonly ILoadScreenSystem _loadScreenSystem;

        private IManager _managerNow;
        
        public void Start()
        {
            ShowMenu();
        }

        #region Subscribe/Unsubscribe

        private void SubscribeOnMenu()
        {
            _messageBus.Subscribe<StartGameMsg>(StartGame);
        }

        private void UnsubscribeOnMenu()
        {
        }

        private void SubscribeOnGame()
        {
        }

        private void UnsubscribeOnGame()
        {
        }

        #endregion

        private void ShowMenu()
        {
            SwitchManager(_menuManager);

            SubscribeOnMenu();
        }
        
        private void ShowGame()
        {
            SwitchManager(_gameManager);

            SubscribeOnGame();
        }

        private void SwitchManager(IManager newManager)
        {
            _loadScreenSystem.Show();
            
            UnsubscribeOnGame();
            UnsubscribeOnMenu();

            _managerNow?.Close();
            _managerNow = newManager;
            _managerNow.Show();
            
            _loadScreenSystem.Hide();
        }

        private void StartGame(StartGameMsg msg)
        {
            ShowGame();
        }
    }
}
using TowerDefence.UI.Messages;
using UnityEngine;
using Utils.UI.Shared.Elements;
using TowerDefence.UI.Shared.BaseClasses;

namespace TowerDefence.UI.Menu.View
{
    public sealed class MenuView : AbstractView
    {
        [SerializeField] private ButtonClassic _startGameButton;
        [SerializeField] private ButtonClassic _exitButton;

        protected override void InitElements()
        {
            base.InitElements();
            
            _startGameButton.Init();
            _exitButton.Init();
        }

        protected override void SubscribeOnButtons()
        {
            _startGameButton.OnClick += StartGame;
            _exitButton.OnClick      += ExitGame;
        }

        private void StartGame() => MessageBus.Publish(new StartGameMsg());
        
        private void ExitGame() => Application.Quit();

        protected override void UnsubscribeOnButtons()
        {
            _startGameButton.OnClick -= StartGame;
            _exitButton.OnClick      -= ExitGame;
        }
    }
}
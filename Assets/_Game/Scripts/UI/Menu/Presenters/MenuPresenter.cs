using System.Threading.Tasks;
using Addressable.Contract;
using Localization.Contract;
using Localization.Shared;
using TowerDefence.UI.Shared.BaseClasses;
using TowerDefence.UI.Shared.Presenter;
using TowerDefence.UI.Shared.View;
using UnityEngine;

namespace TowerDefence.UI.Menu.Presenters
{
    public sealed class MenuPresenter : AbstractPresenter
    {
        private readonly ILocalizationService _localizationService;
        private readonly IMenuAddressableKeys _menuAddressableKeys;

        public MenuPresenter(PresenterDependencies presenterDependencies,
                             ViewDependencies viewDependencies,
                             ILocalizationService localizationService,
                             IMenuAddressableKeys menuAddressableKeys)
        {
            _localizationService = localizationService;
            _menuAddressableKeys = menuAddressableKeys;

            Init(presenterDependencies, viewDependencies);
        }

        public override async Task ShowWindow(Transform parent = null)
        {
            await GetView(_menuAddressableKeys.MainMenu, parent);

            ViewWindow.Show();

            SubscribeOnWindow();
        }

        public override void HideWindow()
        {
            base.HideWindow();
            Addressable.ReleaseAsset(_menuAddressableKeys.MainMenu);
            _localizationService.RemoveAllWrapper(LocalizationWindow.MainMenu);
        }
    }
}
using Addressable.Contract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Addressable.Keys
{
    [CreateAssetMenu(fileName = "MenuAddressableKeys", menuName = "Bombers/Keys/MenuAddressableKeys")]
    public sealed class MenuAddressableKeys : ScriptableObject, IMenuAddressableKeys
    {
        [SerializeField] private AssetReference sf_homeWindow;
        [SerializeField] private AssetReference sf_mainMenu;
        [SerializeField] private AssetReference sf_selectionMenu;
        [SerializeField] private AssetReference sf_onlineGame;
        [SerializeField] private AssetReference sf_offlineGame;
        [SerializeField] private AssetReference sf_shopMenu;
        [SerializeField] private AssetReference sf_settings;
        [SerializeField] private AssetReference sf_controlSettings;
        [SerializeField] private AssetReference sf_soundSettings;
        [SerializeField] private AssetReference sf_screenSettings;
        [SerializeField] private AssetReference sf_descriptionWidow;
        [SerializeField] private AssetReference sf_upgradeWidow;
        
        public string HomeWindow => sf_homeWindow.AssetGUID;
        public string MainMenu => sf_mainMenu.AssetGUID;
        public string SelectionMenu => sf_selectionMenu.AssetGUID;
        public string OnlineGame => sf_onlineGame.AssetGUID;
        public string OfflineGame => sf_offlineGame.AssetGUID;
        public string Settings => sf_settings.AssetGUID;
        public string ShopMenu => sf_shopMenu.AssetGUID;
        public string ControlSettings => sf_controlSettings.AssetGUID;
        public string SoundSettings => sf_soundSettings.AssetGUID;
        public string ScreenSettings => sf_screenSettings.AssetGUID;
        public string DescriptionWindow => sf_descriptionWidow.AssetGUID;
        public string UpgradeWindow => sf_upgradeWidow.AssetGUID;
    }
}
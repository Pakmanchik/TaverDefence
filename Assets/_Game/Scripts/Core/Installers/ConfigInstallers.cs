using Addressable.Contract;
using DI.Contract;
using Localization.Shared;
using TowerDefence.Core.Shared;

namespace TowerDefence.Core.Installers
{
    public sealed class ConfigInstallers
    {
        private readonly IMenuAddressableKeys _menuAddressableKeys;
        private readonly IGameAddressableKeys _gameAddressableKeys;
        private readonly LanguageConfig _languageConfig;

        public ConfigInstallers(ConfigCollection configCollection)
        {
            _menuAddressableKeys = configCollection._menuAddressableKeys;
            _gameAddressableKeys = configCollection._gameAddressableKeys;
            _languageConfig      = configCollection._languageConfig;
        }

        public void InstallTo(IDiContainer diContainer)
        {
            diContainer.BindFromInstance(_menuAddressableKeys);
            diContainer.BindFromInstance(_gameAddressableKeys);
            diContainer.BindFromInstance(_languageConfig);
        }
    }
}
using Addressable.Keys;
using Localization.Shared;
using TowerDefence.Game.Entity.Configs;
using TowerDefence.Game.Entity.Configs.Towers;
using UnityEngine;

namespace TowerDefence.Core.Shared
{
    [CreateAssetMenu(fileName = "CollectionConfig", menuName = "Configs/CollectionConfig")]
    public sealed class ConfigCollection : ScriptableObject
    {
        [SerializeField] public GameAddressableKeys _gameAddressableKeys;
        [SerializeField] public MenuAddressableKeys _menuAddressableKeys;
        [SerializeField] public LanguageConfig _languageConfig;
        [SerializeField] public CollectionTowerConfigs _collectionTowerConfigs;
    }
}
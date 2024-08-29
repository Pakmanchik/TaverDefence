using Addressable.Keys;
using Localization.Shared;
using UnityEngine;

namespace TowerDefence.Core.Shared
{
    [CreateAssetMenu(fileName = "CollectionConfig", menuName = "Configs/CollectionConfig")]
    public sealed class ConfigCollection : ScriptableObject
    {
        [SerializeField] public GameAddressableKeys _gameAddressableKeys;
        [SerializeField] public MenuAddressableKeys _menuAddressableKeys;
        [SerializeField] public LanguageConfig _languageConfig;
    }
}
using Addressable.Contract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Addressable.Keys
{
    [CreateAssetMenu(fileName = "MenuAddressableKeys", menuName = "Addressables/Keys/MenuAddressableKeys")]
    public sealed class MenuAddressableKeys : ScriptableObject, IMenuAddressableKeys
    {
        [SerializeField] private AssetReference _mainMenu;
        
        public string MainMenu => _mainMenu.AssetGUID;
    }
}
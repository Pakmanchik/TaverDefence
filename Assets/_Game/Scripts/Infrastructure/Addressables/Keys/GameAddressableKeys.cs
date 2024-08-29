using Addressable.Contract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Addressable.Keys
{
    [CreateAssetMenu(fileName = "GameAddressableKeys", menuName = "Addressables/Keys/GameAddressableKeys")]
    public sealed class GameAddressableKeys : ScriptableObject, IGameAddressableKeys
    {
        [SerializeField] private AssetReference _inGame;
        
        public string InGame => _inGame.AssetGUID;
    }
}
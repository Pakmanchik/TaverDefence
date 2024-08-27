using Addressable.Contract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Addressable.Keys
{
    [CreateAssetMenu(fileName = "GameAddressableKeys", menuName = "Addressables/Keys/GameAddressableKeys")]
    public sealed class GameAddressableKeys : ScriptableObject, IGameAddressableKeys
    {
        [SerializeField] private AssetReference sf_inGame;
        [SerializeField] private AssetReference sf_onePlayer;
        [SerializeField] private AssetReference sf_twoPlayer;
        [SerializeField] private AssetReference sf_optionMenu;
        [SerializeField] private AssetReference sf_deadScreen;
        [SerializeField] private AssetReference sf_endLevelScreen;
        
        public string InGame => sf_inGame.AssetGUID;
        public string OnePlayer => sf_onePlayer.AssetGUID;
        public string TwoPlayer => sf_twoPlayer.AssetGUID;
        public string OptionMenu => sf_optionMenu.AssetGUID;
        public string EndGameScreen => sf_deadScreen.AssetGUID;
        public string EndLevelScreen => sf_endLevelScreen.AssetGUID;
    }
}
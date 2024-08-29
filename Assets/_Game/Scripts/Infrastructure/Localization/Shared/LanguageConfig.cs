using UnityEngine;

namespace Localization.Shared
{
    [CreateAssetMenu(fileName = "LanguageConfig", menuName = "Configs/LanguageConfig")]
    public sealed class LanguageConfig : ScriptableObject
    {
        [SerializeField] public TextAsset Ru_ru;
        [SerializeField] public TextAsset Eng_eng;
    }
}
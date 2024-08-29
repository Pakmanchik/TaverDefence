using System.Collections.Generic;
using Localization.Contract;
using Localization.Shared;
using UnityEngine;

namespace Localization.Impl
{
    public class LocalizationSwitcher : ILanguageSwitcher
    {
        private readonly LanguageConfig _languageConfig;
        
        public LocalizationSwitcher(LanguageConfig languageConfig)
        {
            _languageConfig = languageConfig;
        }

        public Dictionary<string, TextSettings> GetDictionary(Languages languages)
        {
            var language = languages switch
            {
                Languages.Russian => AssembleDictionary(_languageConfig.Ru_ru),
                _ => AssembleDictionary(_languageConfig.Eng_eng),
            };

            return language;
        }

        private Dictionary<string, TextSettings> AssembleDictionary(TextAsset textAsset)
        {
            Dictionary<string, TextSettings> language = new();

            var data = textAsset.text.Split(new[] { '\n' });

            for (var i = 1; i < data.Length; i++) // Начинаем с 1, чтобы пропустить заголовок
            {
                var row = data[i].Split(new[] { '|' });
                if (row.Length == 3) // Проверка на правильное количество колонок
                {
                    var key   = row[0];
                    var value = row[1];
                    var size  = row[2];

                    var textSettings = new TextSettings(value, int.Parse(size));
                    language.Add(key, textSettings);
                }
            }

            return language;
        }
    }
}
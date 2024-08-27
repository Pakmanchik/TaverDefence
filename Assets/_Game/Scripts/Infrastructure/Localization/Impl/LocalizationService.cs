using System;
using System.Collections.Generic;
using System.Linq;
using Localization.Contract;
using Localization.Shared;

namespace Localization.Impl
{
    public sealed class LocalizationService : ILocalizationService, ILocalizationCollection, ILanguageNow
    {
        private readonly TextSettings _defaultSettings = new("TextNotFound", 36);
        private readonly ILanguageSwitcher _languageSwitcher;

        private readonly Dictionary<WrapperTMP, LocalizationWindow> _wrapperStorage = new();

        private Dictionary<string, TextSettings> _wordStorage = new();

        private Languages _lastLanguage = Languages.None;

        /*public LocalizationService(ILocalizationDataAccessor localizationDataAccessor, LanguageConfig languageConfig)
        {
            _localizationDataAccessor = localizationDataAccessor;
            _languageSwitcher         = new LocalizationSwitcher(languageConfig);

            LoadPresettings();
        }*/

        public Languages LanguagesNow => _lastLanguage;

        private void LoadPresettings()
        {
         //   var result = _localizationDataAccessor.LoadLanguage();
           // SwitchLanguage(result);
        }

        public void SwitchLanguage(Languages languages)
        {
            if (_lastLanguage == languages)
                return;

            _lastLanguage = languages;
           // _localizationDataAccessor.SaveLanguage(languages);

            _wordStorage = _languageSwitcher.GetDictionary(languages);

            ChangeAllText();
        }

        private void ChangeAllText()
        {
            foreach (var wrapper in _wrapperStorage)
            {
                var key = wrapper.Key;
                key.TextSettings = SetValues(key.Id);
            }
        }

        public void AddWrapper(WrapperTMP wrapper, LocalizationWindow localizationWindow)
        {
            if (wrapper == null)
                throw new ArgumentNullException(nameof(wrapper));

            _wrapperStorage.TryAdd(wrapper, localizationWindow);

            ChangeText(wrapper);
        }

        public void ChangeText(WrapperTMP wrapper) => wrapper.TextSettings = SetValues(wrapper.Id);

        public void RemoveWrapper(WrapperTMP wrapper)
        {
            if (wrapper is null || !_wrapperStorage.ContainsKey(wrapper))
                return;

            _wrapperStorage.Remove(wrapper);
        }

        public void RemoveAllWrapper(LocalizationWindow localizationWindow)
        {
            if (localizationWindow == LocalizationWindow.All)
            {
                _wrapperStorage.Clear();
                return;
            }

            // Находим все ключи, которые соответствуют данному значению
            var keysToRemove = _wrapperStorage.Where(pair => pair.Value == localizationWindow)
                .Select(pair => pair.Key)
                .ToList();

            // Удаляем найденные ключи
            foreach (var key in keysToRemove)
                _wrapperStorage.Remove(key);
        }

        private TextSettings SetValues(string id) => _wordStorage.GetValueOrDefault(id, _defaultSettings);
    }
}
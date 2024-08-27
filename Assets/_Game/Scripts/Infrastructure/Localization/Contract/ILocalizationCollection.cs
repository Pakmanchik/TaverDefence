using Localization.Shared;

namespace Localization.Contract
{
    public interface ILocalizationCollection
    {
        public void AddWrapper(WrapperTMP wrapper, LocalizationWindow localizationWindow);
        public void ChangeText(WrapperTMP wrapper);
        public void RemoveWrapper(WrapperTMP wrapper);
    }
}